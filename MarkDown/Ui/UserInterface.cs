using MarkDown.Core;
namespace MarkDown.Ui;

public class UserInterface
{
    private readonly ProgramCore _programCore = new ProgramCore();
    
    private readonly string[] _formatters = new string[]
    {
        "plain",
        "bold",
        "italic",
        "header",
        "link",
        "inline-code"
    };

    private readonly string[] _listFormatters = new string[]
    {
        "ordered-list",
        "unordered-list",
    };

    public bool IsUserExited;

    public void StartMenu()
    {
        Console.WriteLine("Choose a formatter:");
        var userInput = RequestUserInput();

        if (userInput == "!exit")
        {
            _programCore.Exit();
            IsUserExited = true;
            Console.WriteLine("Goodbye!");
        }
        else if (userInput == "new-line")
        {
            _programCore.AddNewLine(userInput);
        }
        else if (userInput == "!help")
        {
            PrintHelpMenu();
        }
        else
        {
            if (_formatters.Contains(userInput))
            {
                RequestStandardMarkdownData(userInput);
            }
            else if (_listFormatters.Contains(userInput))
            {
                RequestListMarkdownData(userInput);
            }
            else
            {
                Console.WriteLine("Error happened. Input !help");
            }
        }
    }

    private void RequestStandardMarkdownData(string command)
    {
        Console.WriteLine("Input your word(or word for link)");
        var userWord = RequestUserInput();

        switch (command)
        {
            case "link":
            {
                Console.WriteLine("Input link address");
                var link = RequestUserInput();
            
                _programCore.FormatLink(command, userWord, link);
                return;
            }
            case "header":
            {
                Console.WriteLine("Input header level");
                var headerLevel = RequestNumber();
            
                _programCore.FormatHeader(command, userWord, headerLevel);
                return;
            }
            default:
                _programCore.Format(command, userWord);
                break;
        }
    }

    private void RequestListMarkdownData(string command)
    {
        var quantityOfStrings = RequestNumber();
        var userWords = new List<string>();
        
        
        for (var i = 0; i < quantityOfStrings; i++)
        {
            var userWord = RequestUserInput();
            userWords.Add(userWord);
        }
        _programCore.Format(command, userWords);
    }

    private void PrintHelpMenu()
    {
        Console.WriteLine($"Available formatters: {String.Join(' ', _formatters)}");
    }
    
    private string RequestUserInput()
    {
        Console.Write(">");
        var userInput = Console.ReadLine()?.Trim() ?? "null";
        return userInput;
    }
    
    private int RequestNumber()
    {
        while (true)
        {
            var notParsedNumber = RequestUserInput();
            if (int.TryParse(notParsedNumber, out var parsedNumber))
            {
                return parsedNumber;
            }
            Console.WriteLine("Invalid value entered");
        }
    }
}