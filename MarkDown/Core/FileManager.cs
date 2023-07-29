namespace MarkDown.Core;

public class FileManager
{
    private readonly string _fullPath;

    public FileManager(string docPath, string fileName = "markdown.md")
    {
        _fullPath = Path.Combine(docPath, fileName);
    }
    
    public void WriteMarkdownDataToFile(string data)
    {
        try
        {
            using var writer = new StreamWriter(_fullPath, true);
            writer.Write(data);
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Directory path invalid");
        }
    }

    public void WriteMarkDownDataToFile(List<string> allData)
    {
        foreach (var data in allData)
        {
            WriteMarkdownDataToFile(data);
        }
        WriteMarkdownDataToFile("/n");
    }
}