using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CropView : MonoBehaviour
{
    [SerializeField, Tooltip("Color multiplied over the sprite while the crop is watered")]
    private Color wateredTint = new Color(0.75f, 0.75f, 0.9f);
    private SpriteRenderer spriteRenderer;
    public CropData Data { get; private set; }
    public Crop Crop { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(CropData data, Crop crop)
    {
        Data = data;
        Crop = crop;
        Refresh();
    }

    public void Refresh()
    {
        spriteRenderer.sprite = Data.GetSpriteFor(Crop);
        spriteRenderer.color = Crop.IsWatered ? wateredTint : Color.white;
    }
}
