using NUnit.Framework;
public class WalletTests
{
    [Test]
    public void NewWalletStartsEmpty()
    {
        Wallet wallet = new Wallet();
        Assert.AreEqual(0, wallet.Money);
    }

    [Test]
    public void AddIncreasesMoney()
    {
        Wallet wallet = new Wallet();
        wallet.Add(50);
        Assert.AreEqual(50, wallet.Money);
    }

    [Test]
    public void AddNegativeAmountFails()
    {
        Wallet wallet = new Wallet();
        Assert.Throws<System.ArgumentOutOfRangeException>(() => wallet.Add(-50));
    }
}