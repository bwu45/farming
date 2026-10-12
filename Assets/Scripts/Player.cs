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
}
