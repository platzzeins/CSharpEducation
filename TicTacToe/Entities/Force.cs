namespace TicTacToe.Entities;

public class Force
{
    public BoardPosition Position { get; }
    public int XLine = 0;
    public int YLine = 0;
    public int Diagonal = 0;
    public int TotalForce => XLine + YLine + Diagonal;

    public Force(BoardPosition position) => Position = position;
    public Force(int x, int y) => Position = new BoardPosition(x, y);
    public override string ToString() => $"{Position} : x - {XLine}; y - {YLine}; diagonal - {Diagonal}";
}