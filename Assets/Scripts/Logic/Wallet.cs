public class Wallet
{
    public int Money { get; private set; }

    public void Add(int amount)
    {
        Money += amount;
    }
}
