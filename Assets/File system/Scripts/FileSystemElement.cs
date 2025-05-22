using System;
using UnityEngine;

public class FileSystemElement
{
    public string Name { get; private set; }
    private Folder Parent;

    // Name constructor
    protected FileSystemElement(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        Name = name;
    }

    // Returns absolute path of the element
    public string GetPath()
    {
        return "/" + Parent.GetPath() + Name;
    }


    // Should NOT be called elsewhere than in Folder class (couldn't find a way to
    // prevent this with good design, help appreciated)
    internal void setParent(Folder newParent)
    {
        Parent = newParent;
    }
}
