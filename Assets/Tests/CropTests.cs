using NUnit.Framework;

public class CropTests
{
    [Test]
    public void WateredCropGrowsAfterAdvance()
    {
        Crop crop = new Crop(3);
        crop.Water();
        crop.AdvanceDay();

        Assert.AreEqual(1, crop.DaysGrown);
    }
}
