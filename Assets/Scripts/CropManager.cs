using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    [SerializeField] private CropView cropPrefab;
    [SerializeField] private CropData[] crops;
    private readonly Dictionary<string, CropData> seedToCrop = new Dictionary<string, CropData>();    
    private readonly Farm farm = new Farm();
    private readonly Dictionary<Vector3Int, CropView> views = new Dictionary<Vector3Int, CropView>();
    private void Awake()
    {
        foreach (CropData crop in crops)
        {
            if (crop.SeedItem == null)
            {
                Debug.LogWarning($"Crop '{crop.CropName}' has no Seed Item, so it can't be planted");
                continue;
            }
            seedToCrop[crop.SeedItem.itemName] = crop;
        }
    }

    public void Start()
    {
        GameManager.instance.dayManager.Cycle.OnDayAdvanced += OnDayAdvanced;
    }

    public void OnDestroy()
    {
        if (GameManager.instance == null || GameManager.instance.dayManager == null) return;
        GameManager.instance.dayManager.Cycle.OnDayAdvanced -= OnDayAdvanced;
    }

    public void OnDayAdvanced(int day)
    {
        farm.AdvanceDay(); 
        foreach (CropView view in views.Values)
        {
            view.Refresh();
        }
    }

    public bool TryPlant(Vector3Int cell, string seedName)
    {
        if (!seedToCrop.TryGetValue(seedName, out CropData data)) return false; //not a seed
        if (!GameManager.instance.tileManager.IsPlowed(cell)) return false; //not plowed 
        Crop crop = data.CreateCrop();
        if (!farm.TryPlant(cell.x, cell.y, crop)) return false; //already planted

        Vector3 pos =  GameManager.instance.tileManager.GetCellCenter(cell);
        CropView view = Instantiate(cropPrefab, pos, Quaternion.identity, transform);
        view.Init(data, crop);

        views.Add(cell, view);
        return true;
    }

    public bool TryWater(Vector3Int cell)
    {
        if (!farm.TryWater(cell.x, cell.y)) return false; //nothing planted here

        views[cell].Refresh();
        return true;
    }
}
