using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TileManager tileManager;
    public ItemManager itemManager;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        tileManager = GetComponent<TileManager>();
        itemManager = GetComponent<ItemManager>();
    }
}
