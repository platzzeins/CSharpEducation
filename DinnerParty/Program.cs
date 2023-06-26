namespace DinnerParty
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var dinnerParty = new DinnerParty();
            if (!dinnerParty.RequestNumberOfFriends()) return;
            dinnerParty.RequestFriends();
            dinnerParty.RequestNumberOfFriends();
            dinnerParty.RequestTotalAmount();
            dinnerParty.AskingIsThereLuckyOne();
            dinnerParty.SetPartedAmountToFriends();
            dinnerParty.PrintEndScreen();
        }
    }
}

