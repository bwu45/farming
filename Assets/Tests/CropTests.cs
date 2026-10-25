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

    [Test]
    public void NewCropIsAtFirstStage()
    {
        Crop crop = new Crop(3);
        Assert.AreEqual(0, crop.GetStage(4));
    }

    [Test]
    public void PartlyGrownCropIsAtMiddleStage()
    {
        Crop crop = new Crop(3);
        crop.Water();
        crop.AdvanceDay();
        Assert.AreEqual(1, crop.GetStage(4));
    }

    [Test]
    public void GetStageWithNoStagesThrows()
    {
        Crop crop = new Crop(3);
        Assert.Throws<System.ArgumentOutOfRangeException>(() => crop.GetStage(0));
    }

    [Test]
    public void HarvestableCropIsAtLastStage()
    {
        Crop crop = new Crop(3);
        for (int i = 0; i < 3; i++)
        {
            crop.Water();
            crop.AdvanceDay();
        }
        Assert.AreEqual(3, crop.GetStage(4));
    }

    [Test]
    public void CropWithZeroDaysToGrowThrows()
    {
        Assert.Throws<System.ArgumentOutOfRangeException>(() => new Crop(0));
    }
}
