using NUnit.Framework;
using UnityEngine;

public class FileSystemTests
{
    private FileSystem fileSystem;

    // Executed before each test, resets the FileSystem by creating a new GameObject and giving it a FileSystem
    [SetUp]
    public void SetUp()
    {
        GameObject fileSystemGameObject = new GameObject("FileSystem");
        fileSystem = fileSystemGameObject.AddComponent<FileSystem>();
    }

    // Executed after each test, destroys the used GameObject
    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(fileSystem.gameObject);
    }

    [Test]
    public void GetElementByPath()
    {
        fileSystem.CreateFileFromPath("root/test folder 1/test folder 2/test file 1");
        fileSystem.CreateFileFromPath("root/test folder 1/test folder 2/test file 2");

        Assert.AreEqual(fileSystem.GetElementByPath("root"), fileSystem.Root);
        Assert.AreEqual(fileSystem.GetElementByPath("root/test folder 1").Name, "test folder 1"); 
        Assert.AreEqual(fileSystem.GetElementByPath("root/test folder 1/test folder 2").Name, "test folder 2");
        Assert.AreEqual(fileSystem.GetElementByPath("root/test folder 1/test folder 2/test file 2").Name, "test file 2");
    }

    [Test]
    public void CreateFile_CheckPath()
    {
        string path = "root/lol";
        File createdFile = fileSystem.CreateFileFromPath(path);
        Assert.AreEqual(createdFile.GetPath(), path);

        path = "root/test folder 1/test folder 2/test file";
        createdFile = fileSystem.CreateFileFromPath(path);
        Assert.AreEqual(createdFile.GetPath(), path);

        path = "root/test folder 1/file.txt";
        createdFile = fileSystem.CreateFileFromPath(path);
        Assert.AreEqual(createdFile.GetPath(), path);
    }

    [Test]
    public void CreateFile_CheckExistingFoldersAreUsed()
    {
        fileSystem.CreateFileFromPath("root/test folder 1/test folder 2/test file 1");
        fileSystem.CreateFileFromPath("root/test folder 1/test folder 2/test file 2");

        Assert.AreEqual(fileSystem.Root.Children.Count, 1);
        Folder testFolder2 = (Folder)fileSystem.GetElementByPath("root/test folder 1/");
        Assert.AreEqual(testFolder2.Children.Count, 1);
    }
}
