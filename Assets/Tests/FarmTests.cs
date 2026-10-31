using NUnit.Framework;

public class FarmTests
{
    [Test]
    public void NewFarmHasNoCrops()
    {
        Farm farm = new Farm();

        Assert.AreEqual(0, farm.CropCount);
    }

    [Test]
    public void PlantingOnEmptyCellSucceeds()
    {
        Farm farm = new Farm();

        bool planted = farm.TryPlant(0, 0, new Crop(3));

        Assert.IsTrue(planted);
        Assert.AreEqual(1, farm.CropCount);
    }

    [Test]
    public void PlantedCellIsOccupied()
    {
        Farm farm = new Farm();
        farm.TryPlant(2, 5, new Crop(3));

        Assert.IsTrue(farm.IsOccupied(2, 5));
    }

    [Test]
    public void CannotPlantOnOccupiedCell()
    {
        Farm farm = new Farm();
        farm.TryPlant(0, 0, new Crop(3));

        bool plantedAgain = farm.TryPlant(0, 0, new Crop(3));

        Assert.IsFalse(plantedAgain);
        Assert.AreEqual(1, farm.CropCount);
    }

    [Test]
    public void DifferentCellsAreIndependent()
    {
        Farm farm = new Farm();

        Assert.IsTrue(farm.TryPlant(0, 0, new Crop(3)));
        Assert.IsTrue(farm.TryPlant(1, 0, new Crop(3)));
        Assert.IsFalse(farm.IsOccupied(0, 1));
        Assert.AreEqual(2, farm.CropCount);
    }

    [Test]
    public void NegativeCellsWork()
    {
        Farm farm = new Farm();
        farm.TryPlant(-4, -2, new Crop(3));

        Assert.IsTrue(farm.IsOccupied(-4, -2));
        Assert.IsFalse(farm.IsOccupied(4, 2));
    }

    [Test]
    public void GetCropReturnsThePlantedCrop()
    {
        Farm farm = new Farm();
        Crop crop = new Crop(3);
        farm.TryPlant(1, 1, crop);

        Assert.AreSame(crop, farm.GetCrop(1, 1));
    }

    [Test]
    public void GetCropOnEmptyCellReturnsNull()
    {
        Farm farm = new Farm();

        Assert.IsNull(farm.GetCrop(1, 1));
    }

    [Test]
    public void PlantingNullCropThrows()
    {
        Farm farm = new Farm();

        Assert.Throws<System.ArgumentNullException>(() => farm.TryPlant(0, 0, null));
    }
}