namespace Maze.Core;

public class LevelFileHandler
{
    private readonly string _path;

    public LevelFileHandler(string folderPath)
    {
        _path = folderPath;
    }
    
    public char[,] Read(string level)
    {
        var lines = File.ReadAllLines(Path.Combine(_path, level));
        var map = new char[lines.Length, lines[0].Length];

        try
        {
            var lineLength = lines[0].Length;
            foreach (var line in lines)
            {
                if (line.Length != lineLength)
                {
                    throw new IndexOutOfRangeException();
                }
            }
            
            for (var i = 0; i < lines.Length; i++)
            {
                for (var j = 0; j < lines[i].Length; j++)
                {
                    map[i, j] = lines[i][j];
                }
            }
            Console.WriteLine("smth6");
            return map;
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Wrong architecture of level!!!");
            return new char[,]{};
        }
        
    }

    public List<string> GetListOfLevels()
    {
        var levels = new List<string>();
        
        foreach (var file in Directory.GetFiles(_path))
        {
            var splitFileName = file.Split('/');
            levels.Add(splitFileName[^1]);
        }

        return levels;
    }
}