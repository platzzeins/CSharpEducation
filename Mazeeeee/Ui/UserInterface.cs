using Maze.Core;
namespace Maze.Ui;

public class UserInterface
{
    public UserInterface(Maze maze)
    {
        
    }
    
    public void ReadUserMove()
    {
        var userMove = Console.ReadKey(true);
        switch (userMove.Key)
        {
            case ConsoleKey.UpArrow:
                
        }
    }
    
    public void PrintMaze(char[,] map)
    {
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                Console.Write(map[i, j]);
            }
            Console.WriteLine();
        }
    }
}