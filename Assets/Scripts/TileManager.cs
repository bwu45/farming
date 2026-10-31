using UnityEngine;
using UnityEngine.Tilemaps;
public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile interactedTile;
    [SerializeField] private Tile visibleInteractableTile;

    void Start()
    {
        foreach (var position in interactableMap.cellBounds.allPositionsWithin)
        {
            if (interactableMap.GetTile(position) == visibleInteractableTile)
                interactableMap.SetTile(position, hiddenInteractableTile);
        }
    }

    public bool IsInteractable(Vector3Int position)
    {
        return interactableMap.GetTile(position) == hiddenInteractableTile;
    }

    public void SetInteracted(Vector3Int position)
    {
        interactableMap.SetTile(position, interactedTile);
    }

    public bool IsPlowed(Vector3Int position) 
    {
        return interactableMap.GetTile(position) == interactedTile;
    }

    public Vector3 GetCellCenter(Vector3Int position)
    {
        return interactableMap.GetCellCenterWorld(position);
    }
}
