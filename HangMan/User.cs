namespace HangMan;

public class User
{
    internal char[] Word { get; set; }
    internal int Tries { get; set; }
    internal char CurrentLetter { get; set; }
    internal char PreviousLetter { get; set; }
}