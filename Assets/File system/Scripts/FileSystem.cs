using System;
using UnityEngine;

// Central class of the file system
// This is where you have to do anything from
public class FileSystem : MonoBehaviour
{
    public Folder Root { get; private set; } = new Folder("root");
 

    /// <summary>
    /// Returns a reference to the FileSystemElement corresponding to the given path. Returns null if not found
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public FileSystemElement GetElementByPath(string path)
    {
        string[] pathList = ValidateAndSplitPath(path);

        // If path ~= "root"
        if (pathList.Length == 1)
            return Root;

        Folder parentFolder = TraversePath(pathList, TraversePath_GetElementByPath);

        // Last element
        return parentFolder.ElementInFolderByName(pathList[pathList.Length - 1]);
    }

    /// <summary>
    /// Parses a path and creates all of its elements that don't already exist
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public File CreateFileFromPath(string path)
    {
        string[] pathList = ValidateAndSplitPath(path);

        if (pathList.Length < 2)
            throw new ArgumentException("Path cannot be shorter than 2 elements (including root)");

        Folder parentFolder = TraversePath(pathList, TraversePath_CreateFileFromPath);

        // Last element of path (is a File)
        string fileName = pathList[pathList.Length - 1];

        // If the file already exists (it shouldn't)
        if (parentFolder.ElementInFolderByName(fileName) != null)
        {
            throw new InvalidOperationException($"An element named '{fileName}' already exists.");
        }
        else
        {
            File newFile = new File(fileName);
            parentFolder.AddChild(newFile);

            return newFile;
        }
    }


    private string[] ValidateAndSplitPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be null or empty.", nameof(path));

        string[] pathList = path.Trim('/').Split('/');
        
        if (pathList[0] != "root")
            throw new ArgumentException("Path should start with root folder", nameof(path));

        return pathList;
    }

    /// <summary>
    /// Traverses a path represented by a string array (e.g., from path.Trim('/').Split('/')).
    /// Starts from the root and iterates through the path segments, applying a custom function at each step.
    /// Skips the first element (assumed to be "root") and stops before the last segment (which is typically a file name).
    /// The traversal function receives the current element, the current parent folder, and the current segment name.
    /// It must return the new parent folder for the next iteration.
    /// </summary>
    /// <param name="pathList">An array representing the path (e.g., ["root", "folderA", "folderB", "file.txt"])</param>
    /// <param name="pathParsingFunc">A function applied at each folder-level segment to control traversal or creation</param>
    /// <returns>The last traversed folder (i.e., the parent of the final element in the path)</returns>
    private Folder TraversePath(string[] pathList, Func<FileSystemElement, Folder, string, Folder> pathParsingFunc)
    {
        Folder parentFolder = Root;
        for (int i = 1; i < pathList.Length - 1; i++)
        {
            string segment = pathList[i];
            FileSystemElement currentElement = parentFolder.ElementInFolderByName(segment);
            parentFolder = pathParsingFunc(currentElement, parentFolder, segment);
        }
        return parentFolder;
    }

    /// <summary>
    /// A traversal function used during file creation. If traversed folders don't exist, it creates them.
    /// If a segment exists and is a folder, it continues the traversal.
    /// If a segment exists but is not a folder, it throws an exception.
    /// </summary>
    /// <param name="currentElement">The element found in the current parent folder at the current segment</param>
    /// <param name="parentFolder">The current parent folder being traversed</param>
    /// <param name="name">The name of the current path segment</param>
    /// <returns>The folder to be used as the next parent in the traversal</returns>
    private Folder TraversePath_CreateFileFromPath(FileSystemElement currentElement, Folder parentFolder, string name)
    {
        if (currentElement != null) // If folder exists
        {
            if (currentElement is Folder folder)
            {
                return folder;
            }
            else
            {
                throw new InvalidOperationException($"An element named '{currentElement.Name}' exists but is not a folder.");
            }
        }
        else // Create the folder if it doesn't exist
        {
            Folder newFolder = new Folder(name);
            parentFolder.AddChild(newFolder);
            return newFolder;
        }
    }

    private Folder TraversePath_GetElementByPath(FileSystemElement currentElement, Folder parentFolder, string name)
    {
        if (currentElement == null)
        {
            return null;
        }
        else
        {
            if (currentElement is Folder folder)
            {
                return folder;
            }
            else
            {
                throw new InvalidOperationException($"An element named '{currentElement.Name}' exists but is not a folder.");
            }
        }
    }

}
