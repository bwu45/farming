using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : MonoBehaviour
{
    public Player player;
    public Transform slotGrid;
    public SlotUI slotPrefab;

    private List<SlotUI> slotUIs = new List<SlotUI>();

    private void Start()
    {
        for (int i = 0; i < player.toolbarSize; i++)
        {
            SlotUI box = Instantiate(slotPrefab, slotGrid);
            box.allowRemove = false;
            slotUIs.Add(box); 
        }

        player.inventory.OnChanged += Refresh;
        player.OnSelectedChanged += UpdateHighlight;
        Refresh();
        UpdateHighlight();
    }

    private void OnDestroy()
    {
        if (player == null) return;

        if (player != null && player.inventory != null) 
            player.inventory.OnChanged -= Refresh;

        player.OnSelectedChanged -= UpdateHighlight;
    }

    private void Refresh()
    {
        for (int i = 0; i < slotUIs.Count; i++)
            slotUIs[i].Set(player.inventory.slots[i]);
    }

    private void UpdateHighlight()
    {
        for (int i = 0; i < slotUIs.Count; i++)
        {
            slotUIs[i].SetHighlight(i == player.SelectedIndex);
        }
    }
}
