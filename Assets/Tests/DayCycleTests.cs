using NUnit.Framework;
public class DayCycleTests
{
    [Test]
    public void NewCycleStartsOnDay1()
    {
        DayCycle cycle = new DayCycle();
        Assert.AreEqual(1, cycle.CurrentDay);
    }

    [Test]
    public void AdvanceFiresEventWithNewDay()
    {
        DayCycle cycle = new DayCycle();
        int receivedDay = 0;
        cycle.OnDayAdvanced += day => receivedDay = day;

        cycle.Advance();

        Assert.AreEqual(2, receivedDay);
    }
}
