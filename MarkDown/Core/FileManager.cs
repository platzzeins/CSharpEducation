namespace MarkDown.Core;

public static class FileManager
{
    private static readonly string DocPath = Environment.CurrentDirectory;

    public static void WriteMarkdownDataToFile(string data)
    {
        var date = DateTime.Now;
        var filePath = $"markdown_{date:yyyy-MM-dd}.md";

        try
        {
            using var writer = new StreamWriter(Path.Combine(DocPath, filePath), true);
            writer.Write(data);
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Troubles with directory path");
        }
    }

    public static void WriteMarkDownDataToFile(List<string> allData)
    {
        var date = DateTime.Now;
        var filePath = $"markdown_{date:yyyy-MM-dd}.md";

        try
        {
            using var writer = new StreamWriter(Path.Combine(DocPath, filePath), true);
            foreach (var data in allData)
            {
                writer.Write(data);
            }
            writer.WriteLine();
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Troubles with directory path");
        }
    }

    public static void WriteLogs(string type, string info)
    {
        const string filePath = "markdown_logs.md";
        var fullPath = Path.Combine(DocPath, filePath);

        try
        {
            if (File.Exists(fullPath))
            {
                using var writer = new StreamWriter(Path.Combine(DocPath, filePath), true);
                writer.WriteLine($"|{type}|{DateTime.Now}|{info}|");
            }
            else
            {
                using var writer = new StreamWriter(Path.Combine(DocPath, filePath), true);
                writer.WriteLine("|Type|Date|Info|");
                writer.WriteLine("|-|-|-|");
                writer.WriteLine($"|{type}|{DateTime.Now}|{info}|");
            }
        }
        catch (DirectoryNotFoundException e)
        {
            Console.WriteLine(e);
        }
    }
}