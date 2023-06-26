namespace DinnerParty;

public interface IDinner
{
    public bool RequestNumberOfFriends();
    public void RequestFriends();
    public void RequestTotalAmount();
    public void AskingIsThereLuckyOne();
    public void SetPartedAmountToFriends();
    public void ChooseLuckyOne();
    public void PrintEndScreen();
}