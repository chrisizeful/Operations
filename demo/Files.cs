using System.Collections.Generic;
using Godot;

namespace Operations;

/// <summary>
/// A file utility class.
/// </summary>
public static class Files
{

    /// <summary>
    /// List all files of <paramref name="type"/> at the given <paramref name="path"/> (non-recursive).
    /// </summary>
    /// <param name="path">The path to a directory.</param>
    /// <param name="type">The type of files to list, or null for all files.</param>
    /// <returns></returns>
    public static List<string> ListFiles(string path, string type = null)
    {
        List<string> files = new();
        DirAccess dir = DirAccess.Open(path);
        dir.IncludeHidden = false;
        dir.IncludeNavigational = false;
        AddFiles(dir, type, files);
        dir.Dispose();
        return files;
    }

    private static void AddFiles(DirAccess dir, string type, List<string> files)
    {
        if (dir.ListDirBegin() != Error.Ok)
            return;
        string next = dir.GetNext();
        while (next != "")
        {
            string path = dir.GetCurrentDir() + "/" + next;
            if (!dir.CurrentIsDir())
                if (type == null || path.EndsWith(type))
                    files.Add(path);
            next = dir.GetNext();
        }
        dir.ListDirEnd();
    }

    /// <summary>
    /// Randomly select a string from the list of files.
    /// </summary>
    /// <param name="files">A list of string paths.</param>
    /// <returns></returns>
    public static string Random(this List<string> files)
    {
        return files[GD.RandRange(0, files.Count - 1)];
    }
}