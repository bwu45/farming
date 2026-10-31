using System.Collections.Generic;
using System;

public class Farm
{
    private readonly Dictionary<(int x, int y), Crop> crops = new Dictionary<(int x, int y), Crop>();

    public int CropCount => crops.Count;

    public bool IsOccupied(int x, int y)
    {
        return crops.ContainsKey((x, y));
    }

    public bool TryPlant(int x, int y, Crop crop)
    {
        if (crop == null)
            throw new ArgumentNullException(nameof(crop));
        if (IsOccupied(x, y)) return false;
        crops.Add((x, y), crop);
        return true;
    }

    public Crop GetCrop(int x, int y)
    {
        return crops.TryGetValue((x, y), out Crop crop) ? crop : null;
    }
}