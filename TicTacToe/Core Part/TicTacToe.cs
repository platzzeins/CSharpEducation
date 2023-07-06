using System.Security.AccessControl;

namespace TicTacToe;

public class TicTacToe
{
    public Sign[][] Board = new Sign[3][];
    public Sign UserSign;
    public Sign ComputerSign;
    public Player Status;
    private WinCells? _winCells;
    private readonly Random _rand = new Random();
    
    public TicTacToe()
    {
        CreateTheBoard();
        AssignStatusAndSigns();
    }
    
    private void AssignStatusAndSigns()
    {
        var players = Enum.GetValues(typeof(Player));
        Status = (Player) players.GetValue(_rand.Next(players.Length));
        switch (Status)
        {
            case Player.Computer:
                UserSign = Sign.O;
                ComputerSign = Sign.X;
                break;
            case Player.User:
                UserSign = Sign.X;
                ComputerSign = Sign.O;
                break;
        }
    }

    public void MakeAMove(int x, int y, Player player)
    {
        Board[x][y] = player switch
        {
            Player.User => UserSign,
            Player.Computer => ComputerSign
        };
    }

    public string IterateThroughLines()
    {
        for (var index = 0; index < Board.Length; index++)
        {
            var line = Board[index];
            var winner = CheckQuantityOfSigns(line);
            if (winner != "None")
            {
                _winCells = new WinCells(index, 0, index, 1, index, 2);
                return winner;
            }
        }

        return "None";
    }

    public string IterateThroughColumns()
    {
        for (var i = 0; i < Board.Length; i++)
        {
            var line = new Sign[Board[i].Length];
            for (var j = 0; j < Board[i].Length; j++)
            {
                line[j] = Board[j][i];
            }

            var winner = CheckQuantityOfSigns(line);
            if (winner != "None")
            {
                _winCells = new WinCells(0, i, 1, i, 2, i);
                return winner;
            }
        }
        return "None";
    }

    public string IterateThroughDiagonals()
    {
        var board = Board;
        var line = new Sign[Board.Length];
        var index = 0;
        
        
        for (var i = 0; i < board.Length; i++)
        {
            index = i;
            line[i] = board[i][i];
        }

        var winner = CheckQuantityOfSigns(line);
        if (winner != "None")
        {
            _winCells = new WinCells(index);
            return winner;
        }

        var reversedBoardList = board.Reverse().ToList();
        var reversedBoardArray = reversedBoardList.Select(row => row.ToArray()).ToArray();
        // var index = 0;
        board = reversedBoardArray;
        
        for (var i = 0; i < board.Length; i++)
        {
            index = i;
            line[i] = board[i][i];
        }
        
        winner = CheckQuantityOfSigns(line);
        if (winner != "None")
        {
            _winCells = new WinCells(index);
            return winner;
        }

        return "None";

    }

    public bool IsDraw()
    {
        var downstrokes = 0;

        foreach (var line in Board)
        {
            foreach (var cell in line)
            {
                if (cell == Sign._)
                {
                    downstrokes++;
                }
            }
        }

        return downstrokes == 0;
    }
    
    
    public string CheckQuantityOfSigns(Sign[] line)
    {
        var countComputerSign = line.Count(sign => sign == ComputerSign);
        var countUserSign = line.Count(sign => sign == UserSign);
        if (countComputerSign == 3)
        {
            return "Computer";
        } 
        return countUserSign == 3 ? "User" : "None";
    }

    private void CreateTheBoard()
    {
        for (var i = 0; i < Board.Length; i++)
        {
            Board[i] = new Sign[3];
            for (var j = 0; j < Board[i].Length; j++)
            {
                Board[i][j] = Sign._;
            }
        }
    }

    public bool IsWinCell(int x, int y)
    {
        if (_winCells is null)
        {
            return false;
        }

        return _winCells.IsWinCell(x, y);
    }
    
}