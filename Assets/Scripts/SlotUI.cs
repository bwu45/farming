using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text countText;
    public Button removeButton;
    public GameObject highlight;
    public bool allowRemove = true;

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
}
