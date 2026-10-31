using UnityEngine;

[CreateAssetMenu(fileName = "New Crop", menuName = "Farming/Crop Data", order = 51)]
public class CropData : ScriptableObject
{
    [SerializeField] private string cropName = "Crop Name";
    [SerializeField, Min(1)] private int daysToGrow = 3;
    [SerializeField, Tooltip("First = just planted, last = ready to harvest")] 
    private Sprite[] growthStages;
    [SerializeField] private ItemData seedItem;
    [SerializeField] private ItemData harvestItem;
    [SerializeField, Min(1)] private int sellPrice = 10;

    //ready only; game code can't change shared crop settings at runtime 
    public string CropName => cropName;
    public int DaysToGrow => daysToGrow;
    public Sprite[] GrowthStages => growthStages;
    public ItemData SeedItem => seedItem;
    public ItemData HarvestItem => harvestItem;
    public int SellPrice => sellPrice;

    public Crop CreateCrop() => new Crop(daysToGrow);
    public Sprite GetSpriteFor(Crop crop) => growthStages[crop.GetStage(growthStages.Length)];
}
