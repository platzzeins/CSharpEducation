namespace TicTacToe;

public class Force
{
    public int XCoordinate { get; set; }
    public int YCoordinate { get; set; }
    public int XLine = 0;
    public int YLine = 0;
    public int Diagonal = 0;
    public int TotalForce => XLine + YLine + Diagonal;

    public Force(int xCoordinate, int yCoordinate)
    {
        XCoordinate = xCoordinate;
        YCoordinate = yCoordinate;
    }

    public override string ToString()
    {
        var techString = $"{XCoordinate}|{YCoordinate} : x - {XLine}; y - {YLine}; diagonal - {Diagonal}";
        return techString;
    }
}