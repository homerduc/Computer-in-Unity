using UnityEngine;

public class File : FileSystemElement
{
    // public string type { get; private set; }

    public string Content { get; private set; }

    // Name constructor
    public File(string name) : base(name)
    {

    }
}
