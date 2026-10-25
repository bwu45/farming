using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CropView : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public CropData Data {  get; private set; }
    public Crop Crop { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }

    public void Init(CropData data)
    {
        Data = data;
        Crop = data.CreateCrop ();
        Refresh();
    }

    public void Refresh()
    {
        spriteRenderer.sprite = Data.GetSpriteFor(Crop);
    }
}
