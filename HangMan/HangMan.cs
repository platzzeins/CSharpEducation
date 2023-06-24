namespace HangMan;

public class HangMan
{
    private readonly string[] _words = new string[]
    {
        "php",
        "java",
        "javascript",
        "python"
    };

    private string _selectedWord;
    private readonly User _user;
    public bool IsTriesLeft => _user.Tries <= 0;
    public string UserWordDisplay => string.Join("", _user.Word);
    
    public HangMan(User user)
    {
        _user = user;
        _user.Tries = 8;
        SetSelectedWord();
        SetBlankUserWord();
    }

    
    ///<summary>
    ///Prints first Welcome screen
    ///</summary>
    public void PrintGreeting()
    {
        Console.WriteLine("HANGMAN");
        Console.WriteLine("The game is available now");
    }
    
    
    ///<summary>
    ///Requesting letter from user and checking its` correctness
    ///</summary>
    public void RequestCharacterFromUser()
    {
        while (true)
        {
            Console.Write(">");
            var userReadLine = Console.ReadLine();
            
            if (userReadLine.Length > 1)
            {
                Console.WriteLine("Input have to only one character in length");
                continue;
            }

            _user.PreviousLetter = _user.CurrentLetter;
            _user.CurrentLetter = userReadLine[0];
            OpenLettersInWord(_user.PreviousLetter);
            break;
        }
    }

    ///<summary>
    ///Replacing underscore with letter that user guessed 
    ///</summary>
    private void OpenLettersInWord(char letter)
    {
        for (var i = 0; i < _selectedWord.Length; i++)
        {
            if (_selectedWord[i] == letter)
            {
                _user.Word[i] = letter;
            }
        }
    }
    
    ///<summary>
    ///Printing _userWord and _tries each time after user attempt
    ///</summary>
    public void PrintUserInterface()
    {
        Console.WriteLine($"Current word: {UserWordDisplay}");
        Console.WriteLine($"Tries left: {_user.Tries}");
    }

    ///<summary>
    ///Printing End Screen for losing variation
    ///</summary>
    public void PrintLoseEndUserInterface()
    {
        Console.WriteLine("You lose!");
        Console.WriteLine($"Right word was: {_selectedWord}");
    }

    ///<summary>
    ///Printing End Screen for winning variation
    ///</summary>
    public void PrintWinEndUserInterface()
    {
        Console.WriteLine("You win!");
        Console.WriteLine($"Tries left: {_user.Tries}");
    }

    ///<summary>
    ///Checking if Word contains Letter in selected word
    ///</summary>
    public bool IsWordContainsLetter(char letter)
    {
        if (_selectedWord.Contains(letter)) return true;
        _user.Tries --;
        Console.WriteLine("This word does not contain this letter");
        return false;
    }

    ///<summary>
    ///Checking if previous letter and current typed letter is the same
    ///</summary>
    public bool IsPreviousLetterTheSame(char letter)
    {
        if (letter != _user.PreviousLetter) return false;
        Console.WriteLine("No improvement!");
        return true;
    }
    
    ///<summary>
    ///Checking if selected word and user word equal to each other
    ///</summary>
    public bool CheckEqualityOfWords()
    {
        if (_user.Word.Contains('_'))
        {
            return false;
        }

        return UserWordDisplay == _selectedWord;
    }

    ///<summary>
    ///Setting random word for the game
    ///</summary>
    private void SetSelectedWord()
    {
        var index = Random.Shared.Next(_words.Length);
        _selectedWord = _words[index];
    }

    ///<summary>
    ///Filling User Word with underscores
    ///</summary>
    private void SetBlankUserWord()
    {
        _user.Word = new char[_selectedWord.Length];
        for (var i = 0; i < _user.Word.Length; i++)
        {
            _user.Word[i] = '_';
        }
    }
}