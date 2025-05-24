using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

// To ensure coherence, only manipulate parents and childrens in this class
public class Folder : FileSystemElement
{
    public List<FileSystemElement> Children { get; private set; } = new List<FileSystemElement>();

    // Name constructor
    public Folder(string name) : base(name)
    {

    }

    /// <summary>
    /// If the folder contains an element named "name", returns it, else null. As is, it is impossible to have two FileSystemElement with the same name in a folder, no matter their type
    /// </summary>
    /// <param name="name"></param>
    /// <returns>The FileSystemElement if found, else null.</returns>
    public FileSystemElement ElementInFolderByName(string name)
    {
        foreach (FileSystemElement element in Children)
        {
            if (element.Name == name)
            {
                return element;
            }
        }
        return null;
    }

    public void AddChild(FileSystemElement child)
    {
        if (Children.Contains(child))
        {
            return;
        }

        Children.Add(child);
        child.setParent(this);
    }

    private void RemoveChild(FileSystemElement child)
    {
        if (Children.Remove(child))
        {
            child.setParent(null);
        }
    }
}
