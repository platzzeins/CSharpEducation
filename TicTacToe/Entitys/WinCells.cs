namespace TicTacToe;

public class WinCells
{
    public int FirstCellXCoordinate;
    public int FirstCellYCoordinate;
    public int SecondCellXCoordinate;
    public int SecondCellYCoordinate;
    public int ThirdCellXCoordinate;
    public int ThirdCellYCoordinate;

    public WinCells(int firstX, int firstY, int secondX, int secondY, int thirdX, int thirdY)
    {
        FirstCellXCoordinate = firstX;
        FirstCellYCoordinate = firstY;
        SecondCellXCoordinate = secondX;
        SecondCellYCoordinate= secondY;
        ThirdCellXCoordinate = thirdX;
        ThirdCellYCoordinate = thirdY;
    }

    public WinCells(int diagonalCoordinate)
    {
        FirstCellXCoordinate = diagonalCoordinate;
        FirstCellYCoordinate = diagonalCoordinate;
        SecondCellXCoordinate = diagonalCoordinate;
        SecondCellYCoordinate= diagonalCoordinate;
        ThirdCellXCoordinate = diagonalCoordinate;
        ThirdCellYCoordinate = diagonalCoordinate;
    }

    public bool IsWinCell(int x, int y)
    {
        return (x == FirstCellXCoordinate || x == SecondCellXCoordinate || x == ThirdCellXCoordinate) &&
               (y == FirstCellYCoordinate || y == SecondCellYCoordinate || y == ThirdCellYCoordinate);
    }
}