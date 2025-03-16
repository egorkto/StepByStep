using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "PrefabTile", menuName = "2D/Tiles/PrefabTile", order = 1)]

public class PrefabTile : TileBase {
    [SerializeField] private GameObject _object;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.gameObject = _object.gameObject;
        base.GetTileData(position, tilemap, ref tileData);
    }
}