public class Crop
{
    public int DaysToGrow { get; }
    public int DaysGrown { get; private set; }
    public bool IsWatered { get; private set; }
    public bool CanHarvest => DaysGrown >= DaysToGrow;

    public Crop(int daysForGrowth)
    {
        DaysToGrow = daysForGrowth;
    }

    public void Water() { IsWatered = true; }

    public void AdvanceDay()
    {
        if (IsWatered && !CanHarvest)
            ++DaysGrown;
        IsWatered = false;
    }
}