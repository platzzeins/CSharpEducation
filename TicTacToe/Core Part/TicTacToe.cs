using System.Security.AccessControl;

namespace TicTacToe;

public class TicTacToe
{
    public string[][] Board = new string[3][];
    public string UserSign;
    public string ComputerSign;
    public string Status;
    private readonly Random _rand = new Random();
    
    public TicTacToe()
    {
        CreateTheBoard();
        AssignStatusAndSigns();
    }
    
    private void AssignStatusAndSigns()
    {
        var players = new string[] {"User", "Computer" };
        var index = _rand.Next(players.Length);
        Status = players[index];
        if (Status == "User")
        {
            UserSign = "X";
            ComputerSign = "O";
        }
        else
        {
            UserSign = "O";
            ComputerSign = "X";
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
        foreach (var line in Board)
        {
            var winner = CheckQuantityOfSigns(line);
            if (winner != "None")
            {
                return winner;
            }
        }

        return "None";
    }

    public string IterateThroughColumns()
    {
        for (var i = 0; i < Board.Length; i++)
        {
            var line = new string[Board[i].Length];
            for (var j = 0; j < Board[i].Length; j++)
            {
                line[j] = Board[j][i];
            }

            var winner = CheckQuantityOfSigns(line);
            if (winner != "None")
            {
                return winner;
            }
        }
        return "None";
    }

    public string IterateThroughDiagonals()
    {
        var board = Board;
        var line = new string[Board.Length];
        
        for (var i = 0; i < board.Length; i++)
        {
            line[i] = board[i][i];
        }

        var winner = CheckQuantityOfSigns(line);
        if (winner != "None")
        {
            return winner;
        }

        var reversedBoardList = board.Reverse().ToList();
        var reversedBoardArray = reversedBoardList.Select(row => row.ToArray()).ToArray();
        board = reversedBoardArray;
        
        for (var i = 0; i < board.Length; i++)
        {
            line[i] = board[i][i];
        }
        
        winner = CheckQuantityOfSigns(line);
        if (winner != "None")
        {
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
                if (cell == "_")
                {
                    downstrokes++;
                }
            }
        }

        return downstrokes == 0;
    }
    
    
    public string CheckQuantityOfSigns(string[] line)
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
            Board[i] = new string[3];
            var signs = new string[] { "O", "X" };
            for (var j = 0; j < Board[i].Length; j++)
            {
                Board[i][j] = "_";
            }
        }
    }
}