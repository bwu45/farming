using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text countText;
    
    public void Set(Inventory.Slot slot)
    {
        if (slot.type == CollectableType.NONE)
        {
            icon.enabled = false;
            countText.text = "";
            return;
        }

        icon.enabled = true;
        icon.sprite = slot.icon;
        countText.text = slot.count > 1 ? slot.count.ToString() : "";
    }
}
