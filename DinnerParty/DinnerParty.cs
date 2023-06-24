using System.Runtime.InteropServices.JavaScript;

namespace DinnerParty;

public class DinnerParty
{
    private int _totalAmount;
    private int _numberOfFriends;
    private Friend[] _friends;
    private Friend _luckyOne;

    /// <summary>
    /// Requesting quantity of friends from user
    /// </summary>
    public void RequestNumberOfFriends()
    {
        Console.WriteLine("Enter the number of friends joining (including you):");
        _numberOfFriends = RequestNumberFromUser();
        if (_numberOfFriends <= 0)
        {
            Console.WriteLine("No one is joining for the party");
            Environment.Exit(0);
        }
        _friends = new Friend[_numberOfFriends];
        RequestFriends();
    }

    /// <summary>
    /// Requesting each friends` name to register it
    /// </summary>
    public void RequestFriends()
    {
        Console.WriteLine("Enter each friends` name");
        for (var i = 0; i < _numberOfFriends; i++)
        {
            Console.Write(">");
            var name = Console.ReadLine();
            var friend = new Friend(name);
            _friends[i] = friend;
        }
    }

    /// <summary>
    /// Requesting total amount of money
    /// </summary>
    public void RequestTotalAmount()
    {
        Console.WriteLine("Enter the total amount:");
        _totalAmount = RequestNumberFromUser();
    }

    /// <summary>
    /// Asking a user, if he/she wants a lucky one
    /// </summary>
    public void AskingIsThereLuckyOne()
    {
        Console.WriteLine("Do you want to use the \"Who is lucky?\" feature? Write Yes/No:");
        var answer = RequestAnswerFromUser();
        if (answer == "yes")
        {
            ChooseLuckyOne();
        }
        SetPartedAmountToFriends();
    }

    /// <summary>
    /// Setting parts to friends
    /// </summary>
    public void SetPartedAmountToFriends()
    {
        if (_luckyOne != null)
        {
            foreach (var friend in _friends)
            {
                if (friend == _luckyOne) { continue; }

                friend.Part = _totalAmount / (_numberOfFriends - 1);
            }
        }
        else
        {
            foreach (var friend in _friends)
            {
                friend.Part = _totalAmount / _numberOfFriends;
            }
        }
    }
    
    /// <summary>
    /// Choosing lucky one
    /// </summary>
    public void ChooseLuckyOne()
    {
        var index = Random.Shared.Next(_friends.Length);
        _luckyOne = _friends[index];
    }

    /// <summary>
    /// Requesting answer from user
    /// </summary>
    /// <returns>String(yes || no)</returns>
    public string RequestAnswerFromUser()
    {
        while (true)
        {
            var answer = Console.ReadLine();
            if (answer.ToLower() == "yes" || answer.ToLower() == "no")
            {
                return answer.ToLower();
            }
            Console.WriteLine("Incorrect value inputted;");
        }
    }
    
    /// <summary>
    /// Printing End Screen
    /// </summary>
    public void PrintEndScreen()
    {
        Console.WriteLine("Here is each part");
        foreach (var friend in _friends)
        {
            Console.WriteLine($"{friend.Name} is paying {friend.Part}");
        }
    }

    /// <summary>
    /// Requesting any number from user
    /// </summary>
    /// <returns>Integer</returns>
    public int RequestNumberFromUser()
    {
        while (true)
        {
            Console.Write(">");
            var userInput = Console.ReadLine();
            if (int.TryParse(userInput, out var parsedNumber))
            {
                return parsedNumber;
            }
            Console.WriteLine("Incorrect value inputted");
        }
    }
}