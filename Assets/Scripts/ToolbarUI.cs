using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : MonoBehaviour
{
    public Player player;
    public Transform slotGrid;
    public SlotUI slotPrefab;
    public int size = 7;

    private List<SlotUI> slotUIs = new List<SlotUI>();

    private void Start()
    {
        for (int i = 0; i < size; i++)
        {
            SlotUI box = Instantiate(slotPrefab, slotGrid);
            box.allowRemove = false;
            slotUIs.Add(box); 
        }

        player.inventory.OnChanged += Refresh;
        Refresh();
    }

    private void OnDestroy()
    {
        if (player != null && player.inventory != null) 
            player.inventory.OnChanged -= Refresh;
    }

    private void Refresh()
    {
        for (int i = 0; i < slotUIs.Count; i++)
            slotUIs[i].Set(player.inventory.slots[i]);
    }
}
