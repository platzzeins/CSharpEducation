namespace Maze.MapEntities;

public class Bonus : ICell
{
    public char Icon { get; } = '*';
    public int Score { get; set; } = 15;
    public int Weight { get; } = 0;
}