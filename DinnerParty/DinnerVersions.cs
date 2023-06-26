namespace DinnerParty;

public class DinnerVersions
{
    public static void DinnerPartyClassic()
    {
        var dinnerParty = new DinnerParty();
        if (!dinnerParty.RequestNumberOfFriends()) return;
        dinnerParty.RequestFriends();
        dinnerParty.RequestTotalAmount();
        dinnerParty.AskingIsThereLuckyOne();
        dinnerParty.SetPartedAmountToFriends();
        dinnerParty.PrintEndScreen();
    }

    public static void DinnerPartyDictionary()
    {
        var dinnerParty = new DinnerPartyDictVersion();
        if (!dinnerParty.RequestNumberOfFriends()) return;
        dinnerParty.RequestFriends();
        dinnerParty.RequestTotalAmount();
        dinnerParty.AskingIsThereLuckyOne();
        dinnerParty.SetPartedAmountToFriends();
        dinnerParty.PrintEndScreen();
    }
}