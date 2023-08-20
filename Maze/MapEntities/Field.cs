namespace Maze.MapEntities;

public class Field : ICell
{
    public char Icon { get; } = '.';
    public int Score { get; set; } = 0;
    public int Weight { get; } = 0;
}