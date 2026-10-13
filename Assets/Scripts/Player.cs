using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;

    public void Awake()
    {
        inventory = new Inventory(21);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3Int position = Vector3Int.FloorToInt(transform.position);

            if (GameManager.instance.tileManager.IsInteractable(position))
            {
                Debug.Log("Interactable Tile");
                GameManager.instance.tileManager.SetInteracted(position);
            }
        }
    }

    public void DropItem(Item item)
    {
        Vector2 spawnLocation = transform.position;

        Vector2 spawnOffset = Random.insideUnitCircle.normalized*1f;

        Item droppedItem = Instantiate(item, spawnLocation + spawnOffset, 
            Quaternion.identity);

        droppedItem.rb2d.AddForce(spawnOffset*.75f, ForceMode2D.Impulse);
    }
}
