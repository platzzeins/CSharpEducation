using MarkDown.Types;

namespace MarkDown.Core;

public class LogsHandler
{
    private readonly string _fullPath;

    public LogsHandler(string docPath, string fileName = "markdown_logs.md")
    {
        _fullPath = Path.Combine(docPath, fileName);
    }

    public void Write(LogType type, string info)
    {
        try
        {
            using var writer = new StreamWriter(_fullPath, true);
            if (!File.Exists(_fullPath))
            {
                writer.WriteLine("|Type|Date|Info|");
                writer.WriteLine("|-|-|-|");
            }
            writer.WriteLine($"|{type}|{DateTime.Now}|{info}|");
        }
        catch (DirectoryNotFoundException e)
        {
            Console.WriteLine(e);
        }
    }
}