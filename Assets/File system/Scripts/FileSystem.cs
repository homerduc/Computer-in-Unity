using UnityEngine;

// Central class of the file system
// This is where you have to do anything from
public class FileSystem : MonoBehaviour
{
    public Folder Root { get; private set; } = new Folder("root");


    // Create file in root folder
    public void CreateFile(string fileName)
    {
        File newFile = new File(fileName);
        Root.AddChild(newFile);
    }

    // Create folder in root folder
    public void CreateFolder(string folderName)
    {
        Folder newFolder = new Folder(folderName);
        Root.AddChild(newFolder);
    }

}
