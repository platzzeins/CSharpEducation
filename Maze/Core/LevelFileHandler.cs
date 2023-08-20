using Maze.MapEntities;

namespace Maze.Core;

public class LevelFileHandler
{
    private readonly string _path;

    public LevelFileHandler(string folderPath)
    {
        _path = folderPath;
    }
    
    public ICell[,] ReadMapCells(string level)
    {
        var lines = File.ReadAllLines(Path.Combine(_path, level));
        var map = new ICell[lines.Length, lines[0].Length];
        
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
                    ICell cell;

                    switch (lines[i][j])
                    {
                        case '@':
                            cell = new Player(i, j);
                            break;
                        case 'X':
                            cell = new Exit();
                            break;
                        case '*':
                            cell = new Bonus();
                            break;    
                        case '.':
                            cell = new Field();
                            break;
                        case '#':
                            cell = new Border();
                            break;
                        default:
                            cell = new Border();
                            break;
                    }
                    map[i, j] = cell;
                }
                
            }
            Console.WriteLine("smth6");
            
            return map;
        }
        finally
        {
            Console.WriteLine("Wrong Level Architecture!");
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