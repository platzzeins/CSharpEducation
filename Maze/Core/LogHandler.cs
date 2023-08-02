using Maze.Entities;

namespace Maze.Core;

public class LogHandler
{
    private readonly string _fullPath;

    public LogHandler(string fileName = "maze_logs.md")
    {
        _fullPath = fileName;
    }
    
    public LogHandler(string docPath, string fileName = "maze_logs.md")
    {
        _fullPath = Path.Combine(docPath, fileName);
    }

    public void Write(LogType type, string info)
    {
        using var writer = new StreamWriter(_fullPath, true);
        if (!File.Exists(_fullPath))
        {
            writer.WriteLine("|Type|Date|Info|");
            writer.WriteLine("|-|-|-|");
        }
        writer.WriteLine($"|{type}|{DateTime.Now}|{info}|");
    }
}