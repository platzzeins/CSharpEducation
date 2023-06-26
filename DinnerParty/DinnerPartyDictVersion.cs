namespace DinnerParty;

public class DinnerPartyDictVersion : IDinner
{
    private Dictionary<string, int> _friends;
    private int _totalAmount;
    private string? _luckyOneName;
    private int _numberOfFriends;

    /// <summary>
    /// Requesting quantity of friends from user
    /// </summary>
    public bool RequestNumberOfFriends()
    {
        Console.WriteLine("Enter the number of friends joining (including you):");
        _numberOfFriends = DinnerParty.RequestNumberFromUser();
        if (_numberOfFriends > 0) return true;
        Console.WriteLine("No one is joining for the party");
        return false;
    }

    /// <summary>
    /// Requesting each friends` name to register it
    /// </summary>
    public void RequestFriends()
    {
        _friends = new Dictionary<string, int>();
        Console.WriteLine("Enter each friends` name");
        for (var i = 0; i < _numberOfFriends; i++)
        {
            Console.Write(">");
            var name = Console.ReadLine().Trim();
            if (CheckNameInFriendList(name))
            {
                Console.WriteLine("This name is already added. Input another one!");
                i--;
                continue;
            }
            _friends.Add(name, 0);
        }
    }

    public bool CheckNameInFriendList(string name)
    {
        return !_friends.ContainsKey(name);
    }

    /// <summary>
    /// Requesting total amount of money
    /// </summary>
    public void RequestTotalAmount()
    {
        Console.WriteLine("Enter the total amount:");
        _totalAmount = DinnerParty.RequestNumberFromUser();
    }

    /// <summary>
    /// Asking a user, if he/she wants a lucky one
    /// </summary>
    public void AskingIsThereLuckyOne()
    {
        Console.WriteLine("Do you want to use the \"Who is lucky?\" feature? Write Yes/No:");
        var answer = DinnerParty.RequestAnswerFromUser();
        if (answer == "yes")
        {
            ChooseLuckyOne();
        }
    }

    /// <summary>
    /// Setting parts to friends
    /// </summary>
    public void SetPartedAmountToFriends()
    {
        if (_luckyOneName != null)
        {
            foreach (var name in _friends.Keys)
            {
                if (_luckyOneName == name) { continue; }
                _friends[name] = _totalAmount / (_numberOfFriends - 1);
            }
        }
        else
        {
            foreach (var name in _friends.Keys)
            {
                _friends[name] = _totalAmount / _numberOfFriends;
            }
        }
    }

    /// <summary>
    /// Choosing lucky one
    /// </summary>
    public void ChooseLuckyOne()
    {
        var names = new List<string>(_friends.Keys);
        var index = Random.Shared.Next(names.Count);
        _luckyOneName = names[index];
    }

    /// <summary>
    /// Printing End Screen
    /// </summary>
    public void PrintEndScreen()
    {
        Console.WriteLine("Here is each part");
        foreach (var friend in _friends)
        {
            Console.WriteLine($"{friend.Key} is paying {friend.Value}");
        }
    }
    
    
}