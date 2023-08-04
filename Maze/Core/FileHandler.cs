namespace Maze.Core;

public class FileHandler
{
    private readonly string _fullPath;
    private readonly StreamWriter _writer;
    
    public FileHandler(string fileName = "maze_table.md")
    {
        _fullPath = fileName;
        _writer = new StreamWriter(_fullPath, true);
        
        var isFileExists = File.Exists(_fullPath);
        if (isFileExists) return;
        _writer.WriteLine("|PlayerName|Level|Time|");
        _writer.WriteLine("|-|-|-|");
    }
    
    public void Write(string playerName, string level, string time)
    {
        _writer.WriteLine($"|{playerName}|{level}|{time}|");
    }

    public void Close()
    {
        _writer.Dispose();
    }
}