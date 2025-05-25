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

        // Going through all of the folders of the path
        Folder parentFolder = Root;
        for (int i = 1; i < pathList.Length - 1; i++)
        {
            string elementName = pathList[i];
            FileSystemElement currentElement = parentFolder.ElementInFolderByName(elementName);

            // Element doesn't exist
            if (currentElement == null)
            {
                return null;
            }
            else
            {
                // Cast to folder if it is one (should be)
                if (currentElement is Folder folder)
                {
                    parentFolder = folder;
                }
                else
                {
                    throw new InvalidOperationException($"An element named '{elementName}' exists but is not a folder.");
                }
            }
        }

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

        // Going through all of the folders of the path
        Folder parentFolder = Root;
        for (int i = 1; i < pathList.Length - 1; i++)
        {
            string elementName = pathList[i];
            FileSystemElement currentElement = parentFolder.ElementInFolderByName(elementName);

            // The Folder already exists
            if (currentElement != null)
            {
                if (currentElement is Folder folder)
                {
                    parentFolder = folder;
                }
                else
                {
                    throw new InvalidOperationException($"An element named '{elementName}' exists but is not a folder.");
                }
            }
            // The Folder does not already exist
            else
            {
                Folder newFolder = new Folder(elementName);
                parentFolder.AddChild(newFolder);
                parentFolder = newFolder;
            }
        }

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
}
