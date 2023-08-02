namespace Maze.Core;

public class Maze
{
    public int PlayerPositionByX { get; set; }
    public int PlayerPositionByY { get; set; }

    public char[,] map { get; private set; }

    public Maze(char[,] map)
    {
        this.map = map;
        FindUserCoordinates();
    }

    public void FindUserCoordinates()
    {
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] != '@') continue;
                PlayerPositionByX = i;
                PlayerPositionByY = j;
                return;
            }
        }
    }
    
}