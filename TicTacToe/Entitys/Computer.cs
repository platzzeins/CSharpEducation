namespace TicTacToe;

public class Computer
{
    private readonly TicTacToe _ticTacToe;
    private readonly string _computerSign;

    public Computer(TicTacToe ticTacToe)
    {
        _ticTacToe = ticTacToe;
        _computerSign = _ticTacToe.ComputerSign;
    }

    public int[] GetCoordinations()
    {
        var length = _ticTacToe.Board.Length;
        var forces = new List<Force>();

        for (var i = 0; i < length; i++)
        {
            for (var j = 0; j < length; j++)
            {
                if (_ticTacToe.Board[i][j] != "_") continue;
                var force = new Force(i, j);
                forces.Add(force);
            }
        }

        CountTheForce(ref forces);
        
        // Debugging code
        // Console.WriteLine($"Computer sign: {_computerSign}");
        // foreach (var force in forces)
        // {
        //     Console.WriteLine(force.ToString());
        // }

        var maxForce = GetMaxForce(forces);

        return new int[] { maxForce.GetX(), maxForce.GetY() };
    }

    private Force GetMaxForce(List<Force> forces)
    {
        return forces.MaxBy(f => f.GetTotalSum());
    }
    
    /// <summary>
    /// Counting the XLine, YLine and Diagonal force for each cell
    /// </summary>
    /// <param name="forces"></param>
    private void CountTheForce(ref List<Force> forces)
    {
        foreach (var force in forces)
        {
            var x = force.GetX();
            var y = force.GetY();
            
            
            foreach (var cell in _ticTacToe.Board[x])
            {
                if (cell == _computerSign)
                {
                    force.xLine += 6;
                }
            }
            
            foreach (var line in _ticTacToe.Board)
            {
                if (line[y] == _computerSign)
                {
                    force.yLine += 6;
                }
            }

            if ((x == 0 && y == 0) || (x == 0 && y == 2) || (x == 2 && y == 0) || (x == 2 && y == 2))
            {
                var board = _ticTacToe.Board;
                if ((x == 2 && y == 0) || (x == 0 && y == 2))
                {
                    board = board.Reverse().ToArray();
                }

                force.diagonal = IterateThroughDiagonals(board);
            }

            if (x == 1 && y == 1)
            {
                var board = _ticTacToe.Board;
                force.diagonal = IterateThroughDiagonals(board);
                
                board = board.Reverse().ToArray();
                force.diagonal = IterateThroughDiagonals(board);
            }
            
        }
    }

    public int IterateThroughLines(int xCoordinate)
    {
        var lineWithSigns = new string[3];

        for (var i = 0; i < _ticTacToe.Board[xCoordinate].Length; i++)
        {
            lineWithSigns[i] = _ticTacToe.Board[xCoordinate][i];
        }
        
        var result = CountSignsInLine(lineWithSigns);

        return result;
    }

    public int IterateThroughColumns(int yCoordinate)
    {
        var lineWithSigns = new string[3];

        for (var i = 0; i < _ticTacToe.Board.Length; i++)
        {
            lineWithSigns[i] = _ticTacToe.Board[i][yCoordinate];
        }

        var result = CountSignsInLine(lineWithSigns);

        return result;
    }
    
    public int IterateThroughDiagonals(string[][] board)
    {
        
        var lineWithSigns = new string[3];
        
        for (var i = 0; i < board.Length; i++)
        {
            lineWithSigns[i] = board[i][i];
        }
        
        var result = CountSignsInLine(lineWithSigns);
        
        return result;
    }

    
    /// <summary>
    /// Counting signs in lines and if there two opponents` signs, add maximum force to the current computer cell
    /// </summary>
    /// <param name="lineWithSigns"></param>
    /// <returns>result: int - How much force will be added</returns>
    private int CountSignsInLine(string[] lineWithSigns)
    {
        var result = 0;
        var resultForComputerSign = lineWithSigns.Count(sign => sign == _computerSign);
        var resultForUserSign = lineWithSigns.Count(sign => sign == _ticTacToe.UserSign);

        if (resultForComputerSign == 2)
        {
            result = 6;
        }

        if (resultForUserSign == 2)
        {
            result = 15;
        }

        return result;
    }
    
}