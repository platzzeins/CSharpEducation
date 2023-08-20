using System.Diagnostics;
using Maze.Entities;
using Maze.MapEntities;

namespace Maze.Core;

public class MazeCore
{
    public Player Player;
    public Direction CurrentDirection = Direction.AtOnePlace;
    private readonly Stopwatch _stopWatch = new ();
    public readonly FileHandler FileHandler = new ();
    public readonly LogHandler LogHandler = new ();
    private readonly LevelFileHandler _levelFileHandler = new ("Levels");
    public ICell[,] Map { get; private set; }
    private string Level { get; set; }
    public bool IsMazeWorking { get; private set; }

    public void Start(string level)
    {
        _stopWatch.Start();
        IsMazeWorking = true;
        Level = level;
        var map = _levelFileHandler.ReadMapCells(Level);
        Map = map;
        
        
        for (var i = 0; i < Map.GetLength(0); i++)
        {
            for (var j = 0; j < Map.GetLength(1); j++)
            {
                if (Map[i, j].GetType() != typeof(Player)) continue;
                Player = new Player(i, j);
                Map[i, j] = new Field();
            }
        }
    }

    public List<string> Levels => _levelFileHandler.GetListOfLevels();

    public string GetElapsedTime()
    {
        var ts = _stopWatch.Elapsed;
        var elapsedTime = string.Format($"{ts.Minutes}:{ts.Seconds}.{ts.Milliseconds / 10}");
        return elapsedTime;
    }
    
    public void WriteFinalResultsToTable(string userName) => FileHandler.Write(userName, Level, GetElapsedTime(), Player.TotalScore);

    public void ChangePlayerPosition()
    {
        while (IsMazeWorking)
        {
            if (CurrentDirection is Direction.Left or Direction.Right)
            {
                Thread.Sleep(200);
            }
            else
            {
                Thread.Sleep(300);
            }
            
            if (IsNextCellBorder())
            {
                continue;
            }
            
            Player.TotalScore += Map[Player.Y, Player.X].Score;

            if (Map[Player.Y, Player.X].GetType() == typeof(Bonus))
            {
                Map[Player.Y, Player.X] = new Field();
            }
            
            switch (CurrentDirection)
            {
                case Direction.Left:
                    Map[Player.Y, Player.X] = new Field();
                    Player.X--;
                    break;
                case Direction.Right:
                    Map[Player.Y, Player.X] = new Field();
                    Player.X++;
                    break;
                case Direction.Down:
                    Map[Player.Y, Player.X] = new Field();
                    Player.Y++;
                    break;
                case Direction.Up:
                    Map[Player.Y, Player.X] = new Field();
                    Player.Y--;
                    break;
                default:
                    continue;
            }

            if (Map[Player.Y, Player.X].GetType() != typeof(Exit)) continue;
            _stopWatch.Stop();
            Player.TotalScore += Map[Player.Y, Player.X].Score;
            IsMazeWorking = false;
            Console.WriteLine("Press Enter");
            return;
        }
    }

    private bool IsNextCellBorder()
    {
        switch (CurrentDirection)
        {
            case Direction.Left when Map[Player.Y, Player.X - 1].Weight > Player.Weight:
            case Direction.Right when Map[Player.Y, Player.X + 1].Weight > Player.Weight:
            case Direction.Down when Map[Player.Y + 1, Player.X ].Weight > Player.Weight:
            case Direction.Up when Map[Player.Y - 1, Player.X].Weight > Player.Weight:
                return true;
        }
    
        return false;
    }
}