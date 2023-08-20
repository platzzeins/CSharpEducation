namespace Maze.MapEntities;

public interface ICell
{
    char Icon { get; }
    int Score { get; }
    int Weight { get; }
}