using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenAreaTileMap : MonoBehaviour
{
    [SerializeField] private TilemapRenderer TileMapToHide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerController playerController))
        {
            TileMapToHide.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController playerController))
        {
            TileMapToHide.enabled = true;
        }
    }
}
