using System;
using System.Linq;
using System.Reflection;
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
    
    /// <summary>
    /// Validators to determine if a <see cref="Operation.Target"/> is ... valid. Contains a Node validator <see cref="IsNodeValid"/> by default.
    /// </summary>
    public static Dictionary<Type, Operation.Validator> TargetValidators = new()
    {
        { typeof(Node), IsNodeValid }
    };

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
    public void Process()
    {
        // Iterate backwards through operations, running and then freeing them
        for (int i = _operations.Count - 1; i >= 0; i--)
        {
            Operation operation = _operations[i];
            if (ProcessSingle(Tree, operation))
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
    /// The default validator that determines if a <see cref="Node"/> is valid.
    /// </summary>
    /// <param name="operation">The operation whose target to check.</param>
    /// <returns>If the target is valid and not queued for deletion.</returns>
    public static bool IsNodeValid(Operation operation)
    {
        Node node = (Node) operation.Target;
        return GodotObject.IsInstanceValid(node) && !node.IsQueuedForDeletion();
    }

    /// <summary>
    /// Process a single operation.
    /// </summary>
    /// <param name="tree">A reference to the SceneTree.</param>
    /// <param name="operation">The operation to process.</param>
    /// <returns>If the operation has finished processing (succeeded, failed, or was cancelled).</returns>
    public static bool ProcessSingle(SceneTree tree, Operation operation)
    {
        // Check pause mode - always/inherit fall through
        if (operation.ProcessMode == Node.ProcessModeEnum.Disabled)
            return false;
        if (!tree.Paused && operation.ProcessMode == Node.ProcessModeEnum.WhenPaused)
            return false;
        if (tree.Paused && operation.ProcessMode == Node.ProcessModeEnum.Pausable)
            return false;
        // Remove if target is invalidated or operation succeeded, failed, or cancelled
        if (operation.Target == null)
            return true;
        if (operation.Current is not Operation.Status.Running and not Operation.Status.Fresh)
            return true;
        if (operation.TargetValidator == null || !operation.TargetValidator.Invoke(operation))
            return true;
        // Run
        operation.Run(tree.Root.GetProcessDeltaTime());
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
}
