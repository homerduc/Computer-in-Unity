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
