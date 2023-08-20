using Maze.Entities;

namespace Maze.Core;

public class LogHandler
{
    private readonly string _fullPath;
    private readonly StreamWriter _writer;

    public LogHandler(string fileName = "maze_logs.md")
    {
        _fullPath = fileName;
        _writer = new StreamWriter(_fullPath, true);
    }

    public void Write(LogType type, string info)
    {
        if (!File.Exists(_fullPath))
        {
            _writer.WriteLine("|Type|Date|Info|");
            _writer.WriteLine("|-|-|-|");
        }
        _writer.WriteLine($"|{type}|{DateTime.Now}|{info}|");
    }

    public void Close()
    {
        _writer.Dispose();
    }
}