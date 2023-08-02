using System.Diagnostics;
using static Maze.Config;
using Maze.Entities;

namespace Maze.Core;

public class MazeCore
{
    public readonly Player Player = new Player();
    public Direction CurrentDirection = Direction.AtOnePlace;
    private readonly Stopwatch _stopWatch = new Stopwatch();
    private readonly FileHandler _fileHandler = new FileHandler();
    private readonly LogHandler _logHandler = new LogHandler();
    private readonly LevelFileHandler _levelFileHandler = new LevelFileHandler(!!!Path to Levels Folder!!!);
    private int ExitCellPositionX { get; set; }
    private int ExitCellPositionY { get; set; }
    public char[,] Map { get; private set; }
    private string Level { get; set; }
    public bool IsMazeWorking { get; private set; }
    private bool IsPlayerWon => (Player.XPosition == ExitCellPositionX && Player.YPosition == ExitCellPositionY);
    

    public void Start(string level)
    {
        _stopWatch.Start();
        IsMazeWorking = true;
        Level = level;
        var map = _levelFileHandler.Read(Level);
        if (map == new char[,]{})
        {
            _logHandler.Write(LogType.Error, $"Wrong architecture of level: {Level}");
            IsMazeWorking = false;
            return;
        }

        Map = map;
        FindExitCoordinates();
        FindUserCoordinates();
    }

    public List<string> Levels => _levelFileHandler.GetListOfLevels();

    private bool IsCurrentCellBonus()
    {
        return Map[Player.YPosition, Player.XPosition] == BonusIcon;
    }

    public string GetElapsedTime()
    {
        var ts = _stopWatch.Elapsed;
        var elapsedTime = string.Format($"{ts.Minutes}:{ts.Seconds}.{ts.Milliseconds / 10}");
        return elapsedTime;
    }
    
    public void WriteFinalResultsToTable(string userName) => _fileHandler.Write("Yan", Level, GetElapsedTime());

    public void ChangePlayerPosition()
    {
        while (IsMazeWorking)
        {
            if (IsPlayerWon)
            {
                _stopWatch.Stop();
                Player.Score += 50;
                IsMazeWorking = false;
                Console.WriteLine("ChangePlayerPosition closed!");
                return;
            }

            if (CurrentDirection == Direction.Left || CurrentDirection == Direction.Right)
            {
                Thread.Sleep(200);
            }
            else
            {
                Thread.Sleep(300);
            }
            

            if (IsCurrentCellBonus())
            {
                Map[Player.YPosition, Player.XPosition] = FieldIcon;
                Player.Score += 15;
            }
            
            if (IsNextCellBorder())
            {
                continue;
            }
            
            switch (CurrentDirection)
            {
                case Direction.Left:
                    Map[Player.YPosition, Player.XPosition] = FieldIcon;
                    Player.XPosition--;
                    break;
                case Direction.Right:
                    Map[Player.YPosition, Player.XPosition] = FieldIcon;
                    Player.XPosition++;
                    break;
                case Direction.Down:
                    Map[Player.YPosition, Player.XPosition] = FieldIcon;
                    Player.YPosition++;
                    break;
                case Direction.Up:
                    Map[Player.YPosition, Player.XPosition] = FieldIcon;
                    Player.YPosition--;
                    break;
                default:
                    continue;
            }
        }
        
    }

    private bool IsNextCellBorder()
    {
        switch (CurrentDirection)
        {
            case Direction.Left when Map[Player.YPosition, Player.XPosition - 1] == BorderIcon:
            case Direction.Right when Map[Player.YPosition, Player.XPosition + 1] == BorderIcon:
            case Direction.Down when Map[Player.YPosition + 1, Player.XPosition ] == BorderIcon:
            case Direction.Up when Map[Player.YPosition - 1, Player.XPosition] == BorderIcon:
                return true;
        }

        return false;
    }

    private void FindUserCoordinates()
    {
        for (var i = 0; i < Map.GetLength(0); i++)
        {
            for (var j = 0; j < Map.GetLength(1); j++)
            {
                if (Map[i, j] != PlayerIcon) continue;
                Player.XPosition = j;
                Player.YPosition = i;
                return;
            }
        }
    }

    private void FindExitCoordinates()
    {
        for (var i = 0; i < Map.GetLength(0); i++)
        {
            for (var j = 0; j < Map.GetLength(1); j++)
            {
                if (Map[i, j] != ExitIcon) continue;
                ExitCellPositionX = j;
                ExitCellPositionY = i;
                return;
            }
        }
    }
    
}