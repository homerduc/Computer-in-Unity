using NUnit.Framework;
using System;
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
        UnityEngine.Object.DestroyImmediate(fileSystem.gameObject);
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
    public void ValidateAndStripPath()
    {
        // "root" should work, but not "root " or " root"
        Assert.AreEqual(fileSystem.GetElementByPath("root"), fileSystem.Root);
        Assert.Catch<ArgumentException>(() => fileSystem.GetElementByPath("root "));
        Assert.Catch<ArgumentException>(() => fileSystem.GetElementByPath(" root"));

        Assert.Catch<ArgumentException>(() => fileSystem.GetElementByPath("notroot/file"));
        Assert.Catch<ArgumentException>(() => fileSystem.GetElementByPath(""));
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

        // "root" folder only has 1 child
        Assert.AreEqual(fileSystem.Root.Children.Count, 1);

        // "test folder 1" only has 1 child
        Folder testFolder2 = (Folder)fileSystem.GetElementByPath("root/test folder 1/");
        Assert.AreEqual(testFolder2.Children.Count, 1);
    }
}
