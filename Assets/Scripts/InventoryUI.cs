using UnityEngine;
using System.Collections.Generic;
public class InventoryUI : MonoBehaviour
{
    public Player player;
    public GameObject panel;
    public Transform slotGrid;
    public SlotUI slotPrefab;

    private List<SlotUI> slotUIs = new List<SlotUI>();

    private void Start()
    {
        for (int i = 0; i < player.inventory.slots.Count; i++)
        {
            SlotUI box = Instantiate(slotPrefab, slotGrid);
            slotUIs.Add(box);
        }

        player.inventory.OnChanged += Refresh;

        Refresh();

        panel.SetActive(false);
    }

    private void OnDestroy()
    {
        if (player != null && player.inventory != null)
            player.inventory.OnChanged -= Refresh;
    }

    //for toggle tab
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            panel.SetActive(!panel.activeSelf);
    }

    //for holding tab
    //private void Update()
    //{
    //    bool held = Input.GetKey(KeyCode.Tab);
    //    if (panel.activeSelf != held) panel.SetActive(held);
    //}

    private void Refresh()
    {
        for (int i = 0; i < slotUIs.Count; i++)
            slotUIs[i].Set(player.inventory.slots[i]); 
    }
}
