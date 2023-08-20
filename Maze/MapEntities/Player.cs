namespace Maze.MapEntities;

public class Player : ICell
{
    public char Icon { get; } = '@';
    public int Score { get; set; } = 0;
    public int TotalScore { get; set; }
    public int Weight { get; } = 0;
    public int X { get; set; }
    public int Y { get; set; }

    public Player(int xCoordinate, int yCoordinate) => (X, Y) = (xCoordinate, yCoordinate);
}