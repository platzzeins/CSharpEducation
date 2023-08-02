namespace Maze.Core;

public class FileHandler
{
    private readonly string _fullPath;

    public FileHandler(string fileName = "maze_table.md")
    {
        _fullPath = fileName;
    }
    
    public FileHandler(string docPath, string fileName = "maze_table.md")
    {
        _fullPath = Path.Combine(docPath, fileName);
    }

    public void Write(string playerName, string level, string time)
    {
        var isFileExists = File.Exists(_fullPath);
        using var writer = new StreamWriter(_fullPath, true);
        if (!isFileExists)
        {
            writer.WriteLine("|PlayerName|Level|Time|");
            writer.WriteLine("|-|-|-|");
        }
        writer.WriteLine($"|{playerName}|{level}|{time}|");
    }
}