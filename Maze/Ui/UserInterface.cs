using Maze.Entities;
using static Maze.Config;

namespace Maze.Ui;
using Core;

public class UserInterface
{
    private readonly MazeCore _maze;
    
    public UserInterface(MazeCore maze)
    {
        _maze = maze;
    }

    public string SelectLevelScreen()
    {
        Console.WriteLine("Choose, what level you want to play");
        var levels = _maze.Levels;

        foreach (var level in levels)
        {
            Console.WriteLine(level);
        }
        
        while (true)
        {
            var userInput = RequestUserInput();

            if (levels.Contains(userInput))
            {
                return userInput;
            }

            Console.WriteLine("Wrong input!");
        }
    }
    public void ReadUserMove()
    {
        while (_maze.IsMazeWorking)
        {
            var userMove = Console.ReadKey(true);
            _maze.CurrentDirection = userMove.Key switch
            {
                ConsoleKey.LeftArrow => Direction.Left,
                ConsoleKey.RightArrow => Direction.Right,
                ConsoleKey.DownArrow => Direction.Down,
                ConsoleKey.UpArrow => Direction.Up,
                _ => _maze.CurrentDirection
            };
        }
        Console.WriteLine("ReadUserMove closed!");
    }
    
    public void PrintFinalScreen()
    {
        Console.WriteLine($"Your score: {_maze.Player.Score}");
        Console.WriteLine($"Your time (min, sec, msec): {_maze.GetElapsedTime()}");
        
        Console.WriteLine("Input your name for table");
        var userName = RequestUserInput();
        
        _maze.WriteFinalResultsToTable(userName);
        
        Console.WriteLine("Thanks for playing!");
    }
    
    public void PrintMaze()
    {
        var map = _maze.Map;
        Console.Clear();
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                var cell = map[i, j];

                if (j == _maze.Player.XPosition && i == _maze.Player.YPosition)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(PlayerIcon);
                    Console.ResetColor();
                    continue;
                }
                Console.ForegroundColor = cell switch
                {
                    FieldIcon => ConsoleColor.Black,
                    BorderIcon => ConsoleColor.Red,
                    ExitIcon => ConsoleColor.Green,
                    _ => Console.ForegroundColor
                };
                Console.Write(map[i, j]);
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }
    
    private string RequestUserInput()
    {
        Console.Write(">");
        var userInput = Console.ReadLine()?.Trim().ToLower() ?? "null";
        return userInput;
    }
}