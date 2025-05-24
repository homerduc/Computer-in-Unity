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

    /// <summary>
    /// Returns absolute path of the element
    /// </summary>
    /// <returns></returns>
    public string GetPath()
    {
        // root folder
        if (Parent == null)
        {
            return Name;
        }
        return $"{Parent.GetPath()}/{Name}";
    }
    // On a ///roottest folder 1test folder 2test file


    /// <summary>
    /// Should NOT be called elsewhere than in Folder class (couldn't find a way to prevent this with good design, help appreciated)
    /// </summary>
    /// <param name="newParent"></param>
    internal void setParent(Folder newParent)
    {
        Parent = newParent;
    }
}
