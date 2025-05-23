using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

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
    public void CreateFile()
    {
        string testFileName = "test file";
        fileSystem.CreateFile(testFileName);
        Assert.AreEqual(fileSystem.Root.Children[0].Name, testFileName);
    }
}
