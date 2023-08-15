namespace Maze.MapEntities;

public class Exit : ICell
{
    public char Icon { get; } = 'X';
    public int Score { get; set; } = 50;
    public int Weight { get; } = -1;
}