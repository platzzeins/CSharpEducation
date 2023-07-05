namespace TicTacToe;

public class Force
{
    private int[] Coordinations;
    public int xLine = 0;
    public int yLine = 0;
    public int diagonal = 0;
    public int TotalForce => xLine + yLine + diagonal;

    public Force(int x, int y)
    {
        Coordinations = new[] { x, y };
    }

    public bool IsEqual(int x, int y)
    {
        return Coordinations[0] == x && Coordinations[1] == y;
    }

    public int GetX()
    {
        return Coordinations[0];
    }

    public int GetY()
    {
        return Coordinations[1];
    }

    public int GetTotalSum()
    {
        return xLine + yLine + diagonal;
    }

    public override string ToString()
    {
        var techString = $"{string.Join(", ",Coordinations)}: x - {xLine}; y - {yLine}; diagonal - {diagonal}";
        return techString;
    }
}