using Maze.Entities;
using Maze.MapEntities;

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

        var row = Console.CursorTop;
        var col = Console.CursorLeft;
        var index = 0;
        while (true)
        {
            DrawMenu(levels, row, col, index);
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.DownArrow:
                    if (index < levels.Count -1)
                        index++;
                    break;
                case ConsoleKey.UpArrow:
                    if (index > 0)
                        index--;
                    break;
                case ConsoleKey.Enter:
                    return levels[index];
            }
        }
    }
    
    private void DrawMenu(List<string> items, int row, int col, int index)
    {
        Console.SetCursorPosition(col, row);
        for (int i = 0; i < items.Count; i++)
        {
            if (i == index)
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.WriteLine(items[i]);
            Console.ResetColor();
        }
        Console.WriteLine();
        Console.ResetColor();
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
    }
    
    public void PrintFinalScreen()
    {
        Console.Clear();
        Console.WriteLine($"Your score: {_maze.Player.TotalScore}");
        Console.WriteLine($"Your time (min, sec, millisecond): {_maze.GetElapsedTime()}");
        
        Console.WriteLine("Input your name for table");
        var userName = RequestUserInput();
        
        _maze.WriteFinalResultsToTable(userName);
        
        Console.WriteLine("Thanks for playing!");
        
        _maze.LogHandler.Close();
        _maze.FileHandler.Close();
    }

    public void ChangePlayerIconPosition(int prevX, int prevY)
    {
        Console.CursorVisible = false;
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(prevX, prevY);
        Console.Write(_maze.Map[prevY, prevX].Icon);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition(_maze.Player.X, _maze.Player.Y);
        Console.Write(_maze.Player.Icon);
        Console.ResetColor();
    }
    
    public void PrintMap()
    {
        var map = _maze.Map;
        Console.Clear();
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                var cell = map[i, j];
        
                if (j == _maze.Player.X && i == _maze.Player.Y)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(_maze.Player.Icon);
                    Console.ResetColor();
                    continue;
                }
                
                Console.ForegroundColor = cell switch
                {
                    Field => ConsoleColor.White,
                    Border => ConsoleColor.Red,
                    Exit => ConsoleColor.Green,
                    _ => Console.ForegroundColor
                };
                Console.Write(map[i, j].Icon);
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