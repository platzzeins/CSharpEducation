namespace DinnerParty
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var dinnerParty = new DinnerParty();
            dinnerParty.RequestNumberOfFriends();
            dinnerParty.RequestTotalAmount();
            dinnerParty.AskingIsThereLuckyOne();
            dinnerParty.PrintEndScreen();
        }
    }
}

