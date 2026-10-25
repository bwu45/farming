using System;
public class Crop
{
    public int DaysToGrow { get; }
    public int DaysGrown { get; private set; }
    public bool IsWatered { get; private set; }
    public bool CanHarvest => DaysGrown >= DaysToGrow;

    public Crop(int daysForGrowth)
    {
        if (daysForGrowth < 1)
            throw new ArgumentOutOfRangeException(nameof(daysForGrowth), "crop needs at least 1 day to grow");
        DaysToGrow = daysForGrowth;
    }

    public void Water() { IsWatered = true; }

    public void AdvanceDay()
    {
        if (IsWatered && !CanHarvest)
            ++DaysGrown;
        IsWatered = false;
    }

    public int GetStage(int stageCount)
    {
        if (stageCount < 1)
            throw new ArgumentOutOfRangeException(nameof(stageCount), "crop needs at least 1 stage");
        if (CanHarvest) return stageCount - 1;

        return DaysGrown * (stageCount - 1)/DaysToGrow;
    }
}