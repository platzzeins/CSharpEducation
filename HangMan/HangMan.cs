namespace HangMan;

public class HangMan
{
    private string[] words = new string[]
    {
        "php",
        "java",
        "javascript",
        "python"
    };

    private string selectedWord;
    public string userWord;

    public HangMan()
    {
        SetSelectedWord();
    }

    public void PrintGreeting()
    {
        Console.WriteLine("HANGMAN");
        Console.WriteLine("The game will be available soon.");
    }

    public bool CheckEqualityOfWords()
    {
        return false;
    }

    private void SetSelectedWord()
    {
        var rand = new Random();
        var index = rand.Next(words.Length);
        selectedWord = words[index];
    }
    
}