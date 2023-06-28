namespace TicTacToe;

public class Computer
{
    private TicTacToe _ticTacToe;

    public Computer(TicTacToe ticTacToe)
    {
        _ticTacToe = ticTacToe;
    }
    

    public void FirstMove()
    {
        if (_ticTacToe.ComputerSign == "X")
        {
            _ticTacToe.Board[1][1] = _ticTacToe.ComputerSign;
            return;
        }

        var userFirstMoveCoordination = new int[2];
        for (var i = 0; i < _ticTacToe.Board.Length; i++)
        {
            for (var j = 0; j < _ticTacToe.Board[i].Length; j++)
            {
                if (_ticTacToe.Board[i][j] != "O") continue;
                userFirstMoveCoordination[0] = i;
                userFirstMoveCoordination[1] = j;
            }
        }
        
        if (userFirstMoveCoordination[0] == 1 && userFirstMoveCoordination[1] == )
    }
}