using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public int toolbarSize = 7;
    public int SelectedIndex { get; private set; } = 0;
    public Inventory.Slot SelectedSlot => inventory.slots[SelectedIndex];
    public event Action OnSelectedChanged;

    public void Awake()
    {
        inventory = new Inventory(21);
    }

    private void Update()
    {
        float scroll = Input.mouseScrollDelta.y;

        for (int i = 0; i < toolbarSize; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                SelectSlot(i);
        }

        if (scroll > 0)
            SelectSlot((SelectedIndex - 1 + toolbarSize) % toolbarSize);
        else if (scroll < 0)
            SelectSlot((SelectedIndex + 1) % toolbarSize);


        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F))
        {
            if (SelectedSlot.itemName != "Hoe") return;

            Vector3Int position = Vector3Int.FloorToInt(transform.position);

            if (GameManager.instance.tileManager.IsInteractable(position))
            {
                Debug.Log("Interactable Tile");
                GameManager.instance.tileManager.SetInteracted(position);
            }
        }
    }

    private void SelectSlot(int index) 
    {
        if (index == SelectedIndex) return;

        SelectedIndex = index;
        OnSelectedChanged?.Invoke();
    }

    public void DropItem(Item item)
    {
        Vector2 spawnLocation = transform.position;
        Vector2 spawnOffset = UnityEngine.Random.insideUnitCircle.normalized*1f;
        Item droppedItem = Instantiate(item, spawnLocation + spawnOffset, 
            Quaternion.identity);

        droppedItem.rb2d.AddForce(spawnOffset*.75f, ForceMode2D.Impulse);
    }
}
