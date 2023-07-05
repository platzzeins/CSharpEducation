namespace TicTacToe;

public class FieldException : Exception
{
    public FieldException(string message) 
        : base(message){}
}