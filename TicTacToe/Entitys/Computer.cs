namespace TicTacToe;

public class Computer
{
    private readonly TicTacToe _ticTacToe;
    private readonly Sign _computerSign;

    public Computer(TicTacToe ticTacToe)
    {
        _ticTacToe = ticTacToe;
        _computerSign = _ticTacToe.ComputerSign;
    }

    public int[] GetCoordinations()
    {
        var length = _ticTacToe.Board.GetLength(0);
        var forces = new List<Force>();

        for (var i = 0; i < length; i++)
        {
            for (var j = 0; j < length; j++)
            {
                if (_ticTacToe.Board[i, j] != Sign._) continue;
                var force = new Force(i, j);
                forces.Add(force);
            }
        }

        CountTheForce(forces);
        
        // Debugging code
        // Console.WriteLine($"Computer sign: {_computerSign}");
        // foreach (var force in forces)
        // {
        //     Console.WriteLine(force.ToString());
        // }

        var maxForce = GetMaxForce(forces);

        return new int[] { maxForce.XCoordinate, maxForce.YCoordinate };
    }

    private Force GetMaxForce(List<Force> forces)
    {
        return forces.MaxBy(f => f.TotalForce);
    }
    
    /// <summary>
    /// Counting the XLine, YLine and Diagonal force for each cell
    /// </summary>
    /// <param name="forces"></param>
    private void CountTheForce(List<Force> forces)
    {
        foreach (var force in forces)
        {
            var x = force.XCoordinate;
            var y = force.YCoordinate;


            force.XLine += IterateThroughLines(x);
            
            force.YLine += IterateThroughColumns(y);

            
            if ((x == 2 && y == 0) || (x == 0 && y == 2))
            {
                force.Diagonal += IterateThroughDiagonals(_ticTacToe.Board, Diagonal.Side);
            }
            
            if ((x == 0 && y == 0) || (x == 2 && y == 2))
            {
                force.Diagonal += IterateThroughDiagonals(_ticTacToe.Board, Diagonal.General);
            }

            if (x == 1 && y == 1)
            {
                var board = _ticTacToe.Board;
                force.Diagonal += IterateThroughDiagonals(board, Diagonal.General);
                
                force.Diagonal += IterateThroughDiagonals(board, Diagonal.Side);
            }
            
        }
    }

    public int IterateThroughLines(int xCoordinate)
    {
        var lineWithSigns = new Sign[3];

        for (var i = 0; i < _ticTacToe.Board.GetLength(0); i++)
        {
            lineWithSigns[i] = _ticTacToe.Board[xCoordinate, i];
        }
        
        return CountSignsInLine(lineWithSigns);
    }

    public int IterateThroughColumns(int yCoordinate)
    {
        var lineWithSigns = new Sign[3];

        for (var i = 0; i < _ticTacToe.Board.GetLength(0); i++)
        {
            lineWithSigns[i] = _ticTacToe.Board[i, yCoordinate];
        }

        return CountSignsInLine(lineWithSigns);
    }
    
    public int IterateThroughDiagonals(Sign[,] board, Diagonal diagonal)
    {
        var lineWithSigns = new Sign[3];
        
        
        if (diagonal == Diagonal.General)
        {
            for (var i = 0; i < board.GetLength(0); i++)
            {
                lineWithSigns[i] = board[i, i];
            }
        }
        if (diagonal == Diagonal.Side)
        {
            var i = 2;
            for (var j = 0; j < _ticTacToe.Board.GetLength(0); j++)
            {
                lineWithSigns[i] = _ticTacToe.Board[j, i];
                i--;
            }
            // foreach (var line in _ticTacToe.Board)
            // {
            //     lineWithSigns[i] = line[i];
            //     
            // }
        }

        return CountSignsInLine(lineWithSigns);
    }

    
    /// <summary>
    /// Counting signs in lines and if there two opponents` signs, add maximum force to the current computer cell
    /// </summary>
    /// <param name="lineWithSigns"></param>
    /// <returns>result: int - How much force will be added</returns>
    private int CountSignsInLine(Sign[] lineWithSigns)
    {
        var result = 0;
        var resultForComputerSign = lineWithSigns.Count(sign => sign == _computerSign);
        var resultForUserSign = lineWithSigns.Count(sign => sign == _ticTacToe.UserSign);

        if (resultForComputerSign == 2)
        {
            result = 12;
        }
        
        if (resultForUserSign == 2)
        {
            result = 11;
        }

        return result;
    }
    
}