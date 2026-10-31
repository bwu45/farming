using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public int toolbarSize = 7;
    public int SelectedIndex { get; private set; } = 0;
    public Inventory.Slot SelectedSlot => inventory.slots[SelectedIndex];
    public event Action OnSelectedChanged;
    private Animator animator;

    public void Awake()
    {
        inventory = new Inventory(21);
        animator = GetComponentInChildren<Animator>();
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

        if(Input.GetKeyDown(KeyCode.Q)) DropFromSlot(SelectedIndex);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F))
        {
            Vector3Int position = Vector3Int.FloorToInt(transform.position);
            if (SelectedSlot.itemName == "Hoe")
            {
                animator.SetTrigger("hoe");

                if (GameManager.instance.tileManager.IsInteractable(position))
                {
                    Debug.Log("Interactable Tile");
                    GameManager.instance.tileManager.SetInteracted(position);
                }
            }
            else if (GameManager.instance.cropManager.TryPlant(position, SelectedSlot.itemName))
                inventory.Remove(SelectedIndex);
        }
    }

    private void SelectSlot(int index) 
    {
        if (index == SelectedIndex) return;

        SelectedIndex = index;
        OnSelectedChanged?.Invoke();
    }

    public void DropFromSlot(int i)
    {
        Inventory.Slot slot = inventory.slots[i];
        if (slot.itemName == "") return;

        Item prefab = GameManager.instance.itemManager.GetItemByName(slot.itemName);
        if (prefab == null)
        {
            Debug.LogWarning($"No prefab is registered in ItemManager for '{slot.itemName}'");
            return;
        }

        DropItem(prefab);
        inventory.Remove(i);
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
