using UnityEngine;

public class FileSystemTests : FileSystem
{
    private void Start()
    {
        RunAllTests();
    }

    void RunAllTests()
    {
        Debug.Log("Starting tests");
        FileTests();
        FolderTests();
    }

    private void FileTests()
    {
        CreateFileTest();
    }

    private void CreateFileTest()
    {
        string createFileTestName = "test file";

        Debug.Log("Creating a file in root folder :");
        try
        {
            CreateFile(createFileTestName);
        }
        catch (System.Exception e)
        {
            Debug.Log("Error :");
            Debug.Log(e.Message);
        }
        if (Root.Children[0].Name == createFileTestName)
        {
            Debug.Log("File created.");
        }
        else Debug.Log("Error in creating file test");
    }

    private void FolderTests()
    {
        CreateFolderTest();
    }

    private void CreateFolderTest()
    {
        string createFolderTestName = "test folder";

        Debug.Log("Creating a folder in root folder :");
        try
        {
            CreateFolder(createFolderTestName);
        }
        catch (System.Exception e)
        {
            Debug.Log("Error :");
            Debug.Log(e.Message);
        }
        if (Root.Children[1].Name == createFolderTestName)
        {
            Debug.Log("Folder created.");
        }
        else Debug.Log("Error in creating folder test");
    }
}
