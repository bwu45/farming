using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    public Image icon;
    public TMP_Text countText;
    public Button removeButton;
    public GameObject highlight;
    public bool allowRemove = true;
    public RectTransform dropZone;
    public System.Action onDroppedOutside;
    private Image dragIcon;
    public int index;
    public System.Action<int> onDropFrom;

    public void Set(Inventory.Slot slot)
    {
        if (slot.itemName == "")
        {
            icon.enabled = false;
            countText.text = "";
            removeButton.gameObject.SetActive(false);
            return;
        }

        icon.enabled = true;
        icon.sprite = slot.icon;
        countText.text = slot.count > 1 ? slot.count.ToString() : "";
        removeButton.gameObject.SetActive(allowRemove);
    }

    public void SetHighlight(bool on)
    {
        highlight.SetActive(on);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!icon.enabled || dropZone == null) return;
        Canvas canvas = GetComponentInParent<Canvas>().rootCanvas;
        dragIcon = new GameObject("DragIcon").AddComponent<Image>();
        dragIcon.sprite = icon.sprite;
        dragIcon.rectTransform.sizeDelta = icon.rectTransform.rect.size;
        dragIcon.transform.SetParent(canvas.transform, false);
        dragIcon.transform.SetAsLastSibling();
        dragIcon.raycastTarget = false;
        dragIcon.transform.position = eventData.position;
    }
    public void OnDrag(PointerEventData eventData) 
    {
        if (dragIcon != null) dragIcon.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragIcon == null) return;
        Destroy(dragIcon.gameObject);

        bool inside = dropZone != null && 
            RectTransformUtility.RectangleContainsScreenPoint(dropZone, eventData.position);

        if (!inside)
            onDroppedOutside?.Invoke();
    }

    public void OnDrop(PointerEventData eventData)
    {
        SlotUI source = eventData.pointerDrag.GetComponent<SlotUI>();
        if (source == null || source.dragIcon == null) return;
        onDropFrom?.Invoke(source.index);
    }
}
