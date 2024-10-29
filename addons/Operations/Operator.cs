using System;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Godot;

namespace Operations;

/// <summary>
/// An operator is responsible for running a list of operations. It also provides static methods
/// for running individual operations and loading operations from files.
/// </summary>
public partial class Operator
{

    private static readonly Dictionary<string, Type> _aliases = new();
    /// <summary>
    /// Shorthand names for classes to be used in operation files. These are normally set via
    /// an <see cref="OperationAttribute"/> but can be overriden using this dictionary.
    /// </summary>
    public static IReadOnlyDictionary<string, Type> Aliases => _aliases;

    private List<Operation> _operations = new();
    /// <summary>
    /// All of the operations currently being processed.
    /// </summary>
    public IReadOnlyList<Operation> Operations => _operations;

    /// <summary>
    /// A SceneTree reference for determining if operations should be processed in accordance with their <see cref="Operation.ProcessMode"/>.
    /// The <see cref="SceneTree.Root"/> is also used as a target if one is not set on an operation.
    /// </summary>
    public SceneTree Tree { get; private set; }

    public Operator(SceneTree tree)
    {
        Tree = tree;
        if (_aliases.Count == 0)
            Register(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Processes all added operations.
    /// </summary>
    /// <param name="delta">Delta time between frames.</param>
    public void Process(double delta)
    {
        // Iterate backwards through operations, running and then freeing them
        for (int i = _operations.Count - 1; i >= 0; i--)
        {
            Operation operation = _operations[i];
            if (Process(delta, operation))
            {
                _operations.RemoveAt(i);
                Pools.Free(operation);
            }
        }        
    }

    /// <summary>
    /// Adds an operation to the list to process. If the operation lacks a target, it is set to <see cref="SceneTree.Root"/>.
    /// </summary>
    /// <param name="operation">The operation to add.</param>
    public void Add(Operation operation)
    {
        if (operation.Target == null)
            operation.SetTarget(Tree.Root);
        _operations.Add(operation);
    }

    /// <summary>
    /// Process a single operation.
    /// </summary>
    /// <param name="delta">Delta time between frames.</param>
    /// <param name="operation">The operation to process.</param>
    /// <returns>If the operation has finished processing (succeeded, failed, or was cancelled).</returns>
    public bool Process(double delta, Operation operation)
    {
        // Check pause mode - always/inherit fall through
        if (operation.ProcessMode == Node.ProcessModeEnum.Disabled)
            return false;
        if (!Tree.Paused && operation.ProcessMode == Node.ProcessModeEnum.WhenPaused)
            return false;
        if (Tree.Paused && operation.ProcessMode == Node.ProcessModeEnum.Pausable)
            return false;
        // Remove if target is invalidated or operation succeeded, failed, or cancelled
        if (operation.Target == null)
            return true;
        if (operation.Current is not Operation.Status.Running and not Operation.Status.Fresh)
            return true;
        if (!operation.TargetValidator.Invoke(operation.Target))
            return true;
        // Run
        operation.Run(delta);
        return false;
    }

    /// <summary>
    /// Register all <see cref="Operation"/> classes in the provided assembly that have an <see cref="OperationAttribute"/>.
    /// </summary>
    /// <param name="assembly">The assembly containing Operations.</param>
    public static void Register(Assembly assembly)
    {
        assembly.GetTypes()
            .Where(t => typeof(Operation).IsAssignableFrom(t))
            .Select(a => (a, a.GetCustomAttribute<OperationAttribute>()))
            .Where(pair => pair.Item2 != null).ToList()
            .ForEach(pair => _aliases[pair.Item2.Shorthand] = pair.a);
    }

    /// <summary>
    /// Opens an operation file.
    /// </summary>
    /// <param name="path">The path to the operation file.</param>
    /// <param name="target">The object the returned operation should target.</param>
    /// <returns></returns>
    public static Operation Open(string path, object target = null) => Load(Files.GetAsText(path), target);
    /// <summary>
    /// Loads an operation from the provided <paramref name="fileAsText"/>.
    /// </summary>
    /// <param name="fileAsText">The file as raw text. This is NOT a path.</param>
    /// <param name="target">The object the returned operation should target.</param>
    /// <returns></returns>
    public static Operation Load(string fileAsText, object target = null)
    {
        string[] lines = fileAsText.Trim().Split('\n');
        int index = 0;
        // Find first non-comment, valid line
        while (lines[index].StartsWith("#") || lines[index].Trim() == "")
            index++;
        // Defined operations by line
        Dictionary<int, Operation> ops = new();
        try
        {
            // Use first line as root
            Operation root = LoadOperation(lines[index].Trim(), target);
            ops[index] = root;
            // Iterate over all other lines
            for (int i = index + 1; i < lines.Length; i++)
            {
                if (lines[i].Trim().StartsWith("#"))
                    continue; // Skip comments
                // Find parent operation by locating first line with less indentation
                Operation parent = null;
                int indent = lines[i].TakeWhile(char.IsWhiteSpace).Count();
                for (int j = i; j >= index; j--)
                {
                    if (lines[j].Trim().StartsWith("#"))
                        continue; // Skip comments
                    int current = lines[j].TakeWhile(char.IsWhiteSpace).Count();
                    if (current == indent - 2)
                    {
                        parent = ops[j];
                        break;
                    }
                }
                // Load child operation
                Operation child = LoadOperation(lines[i].Trim(), target);
                ops[i] = child;
                parent.Children.Add(child);
                // Set target on parent/guard/children
                parent.SetTarget(parent.Target);
            }
            // Set target on root/guard/children
            if (target != null)
                root.SetTarget(root.Target ?? target);
            return root;
        }
        catch (Exception e)
        {
            throw new Exception($"Operator: Error parsing operation on line {index + 1}: ", e);
        }
    }

    private static Operation LoadOperation(string input, object target)
    {
        // Trim comment out
        if (input.Contains("#"))
            input = input[..(input.IndexOf("#") - 1)];
        // Check for a guard(s), subsequent guards will guard the guard before it
        Operation current = null;
        while (input.StartsWith("("))
        {
            Operation guard = LoadOperation(input[1..input.IndexOf(")")], target);
            input = input[(input.IndexOf(")") + 2)..];
            guard.Guard = current;
            current = guard;
        }
        // Create operation from type pool, check for type by alias->fully qualified name
        string[] split = input.Split(new[] { ' ' }, 2);
        if (!_aliases.TryGetValue(split[0], out var type))
            type = Type.GetType(split[0]);
        if (type == null)
            throw new Exception($"Type {split[0]} not found");
        Operation operation = (Operation) Pools.Obtain(type);
        operation.Guard = current;
        // Check if line has any fields
        if (split.Length > 1)
        {
            // Split into fields by spaces, except if they are enclosed in quotes
            string[] fields = Regex.Split(split[1], "(?<=^[^\"]*(?:\"[^\"]*\"[^\"]*)*) (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            for (int i = 0; i < fields.Length; i++)
            {
                // Split field to name/value
                string[] field = fields[i].Split(new[] { ':' }, 2);
                // Special Target case
                if (field[0].ToLower() == "target")
                {
                    operation.SetTarget(((Node) target).GetNode(field[1])); // TODO Resolve entities
                    continue;
                }
                // Try to set field
                FieldInfo info = type.GetField(field[0]);
                if (info != null)
                {
                    info.SetValue(operation, Convert(info.FieldType, field[1]));
                    continue;
                }
                // Try property
                PropertyInfo prop = type.GetProperty(field[0]) ?? throw new Exception($"Parameter {field[0]} not found");
                prop.SetValue(operation, Convert(prop.PropertyType, field[1]));
            }
        }
        return operation;
    }

    private static object Convert(Type type, string value)
    {
        // Check for variant
        if (type == typeof(Variant))
            return ConvertVariant(value);
        // Use string or type converter
        IStringConverter stringConvert = SaveLoad.Instance.Serializer.GetConverter(type);
        if (stringConvert != null)
            return stringConvert.Convert(value);
        TypeConverter converter = TypeDescriptor.GetConverter(type);
        return converter.ConvertFromString(value.Trim('"'));
    }

    private static object ConvertVariant(string value)
    {
        // String must be encapsulated in quotes
        if (value.StartsWith("\"") && value.EndsWith("\""))
            return Variant.CreateFrom(value.Substr(1, value.Length - 2));
        if (bool.TryParse(value, out var bresult))
            return Variant.CreateFrom(bresult);
        if (int.TryParse(value, out var iresult))
            return Variant.CreateFrom(iresult);
        if (float.TryParse(value, out var fresult))
            return Variant.CreateFrom(fresult);
        if (double.TryParse(value, out var dresult))
            return Variant.CreateFrom(dresult);
        if (char.TryParse(value, out var cresult))
            return Variant.CreateFrom(cresult);
        if (sbyte.TryParse(value, out var sbresult))
            return Variant.CreateFrom(sbresult);
        if (short.TryParse(value, out var sresult))
            return Variant.CreateFrom(sresult);
        if (long.TryParse(value, out var lresult))
            return Variant.CreateFrom(lresult);
        if (byte.TryParse(value, out var byresult))
            return Variant.CreateFrom(byresult);
        if (ushort.TryParse(value, out var usresult))
            return Variant.CreateFrom(usresult);
        if (uint.TryParse(value, out var uiresult))
            return Variant.CreateFrom(uiresult);
        if (ulong.TryParse(value, out var ulresult))
            return Variant.CreateFrom(ulresult);
        return null;
    }
}
