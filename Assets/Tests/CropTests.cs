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

    [Test]
    public void UnwateredCropDoesNotGrowAfterAdvance()
    {
        Crop crop = new Crop(3);
        crop.AdvanceDay();

        Assert.AreEqual(0, crop.DaysGrown);
    }

    [Test]
    public void WateredCropDriesOutOvernight()
    {
        Crop crop = new Crop(3);
        crop.Water();
        crop.AdvanceDay();

        Assert.IsFalse(crop.IsWatered);
    }

    [Test]
    public void ReadyCropIsHarvestable()
    {
        Crop crop = new Crop(2);
        crop.Water();
        crop.AdvanceDay();
        Assert.IsFalse(crop.CanHarvest);

        crop.Water();
        crop.AdvanceDay();
        Assert.IsTrue(crop.CanHarvest);
    }
}
