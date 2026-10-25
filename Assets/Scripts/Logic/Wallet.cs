using System;

public class Wallet
{
    public int Money { get; private set; }

    public void Add(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount can't be negative");
        Money += amount;
    }
}
