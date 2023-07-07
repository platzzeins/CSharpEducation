namespace TicTacToe.Entities;

public class WinCells
{
    private readonly BoardPosition[] _positions = new BoardPosition[3];

    public WinCells(int firstX, int firstY, int secondX, int secondY, int thirdX, int thirdY)
    {
        _positions[0] = new BoardPosition(firstX, firstY);
        _positions[1] = new BoardPosition(secondX, secondY);
        _positions[2] = new BoardPosition(thirdX, thirdY);
    }

    public WinCells(int diagonalCoordinate)
    {
        for (var i = 0; i < _positions.Length; i++)
        {
            _positions[i] = new BoardPosition(diagonalCoordinate, diagonalCoordinate);
        }
    }

    public bool IsWinCell(int x, int y)
    {
        return _positions[0].X == x && _positions[0].Y == y ||
               _positions[1].X == x && _positions[1].Y == y ||
               _positions[2].X == x && _positions[2].Y == y;
    }
}