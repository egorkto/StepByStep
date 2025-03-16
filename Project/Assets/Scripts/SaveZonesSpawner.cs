using UnityEngine;
using UnityEngine.Tilemaps;

public class SaveZoneSpawner
{
    private Tilemap _map;
    private TileBase _innerTile;
    private TileBase _edgeTile;
    private TileBase _cornerTile;
    private TileBase _originTile;
    private Vector2Int _size;

    public SaveZoneSpawner(Tilemap tilemap, TileBase innerTile, TileBase edgeTile, TileBase cornerTile, TileBase originTile) {
        _map = tilemap;
        _innerTile = innerTile;
        _edgeTile = edgeTile;
        _cornerTile = cornerTile;
        _originTile = originTile;
    }

    public void SpawnPlatform(Vector2Int size, ref Vector3Int position)
    {
        _size = size;
        var startPosition = position;
        for (int y = 0; y < _size.y; y++) {
            for (int x = 0; x < _size.x; x++) {
                var tileToInstantiate = GetTilePrefab(x, y);
                var rotation = GetTileRotation(x, y);
                CreateTile(tileToInstantiate, position, rotation);
                position.x++;
            }
            position.y++;
            position.x = startPosition.x;
        }
        
    }

    public PlatformOrigin SpawnPlatformWithOrigin(Vector2Int size, ref Vector3Int position) {
        var startPosition = position;
        SpawnPlatform(size, ref position);
        var minTeleportPosition = new Vector2Int(startPosition.x + 1, startPosition.y + 1);
        var maxTeleportPosition = new Vector2Int(startPosition.x + size.x - 2, startPosition.y + size.y - 2);
        return SpawnOriginInRandomPosition(_originTile, minTeleportPosition, maxTeleportPosition);
    }

    private PlatformOrigin SpawnOriginInRandomPosition(TileBase tile, Vector2Int from, Vector2Int to) {
        var x = Random.Range(from.x, to.x + 1);
        var y = Random.Range(from.y, to.y + 1);
        CreateTile(tile, new Vector3Int(x, y, 0), Quaternion.identity);
        return _map.GetInstantiatedObject(new Vector3Int(x, y, 0)).GetComponent<PlatformOrigin>();
    }

    private TileBase GetTilePrefab(int x, int y)
    {
        if (IsCorner(x, y)) return _cornerTile;
        if (IsEdge(x, y)) return _edgeTile;
        return _innerTile;
    }

    Quaternion GetTileRotation(int x, int y)
    {
        if (IsCorner(x, y))
        {
            if (x == 0 && y == 0) return Quaternion.Euler(0, 90, 0);
            if (x == _size.x - 1 && y == 0) return Quaternion.Euler(0, 180, 0);
            if (x == 0 && y == _size.y - 1) return Quaternion.identity;
            if (x == _size.x - 1 && y == _size.y - 1) return Quaternion.Euler(0, 270, 0);
        }

        if (IsEdge(x, y))
        {
            if (x == 0) return Quaternion.identity;
            if (x == _size.x - 1) return Quaternion.Euler(0, 180, 0);
            if (y == 0) return Quaternion.Euler(0, 90, 0);
            if (y == _size.y - 1) return Quaternion.Euler(0, 270, 0);
        }

        return Quaternion.identity;
    }

    bool IsCorner(int x, int y)
    {
        return (x == 0 && y == 0) || (x == 0 && y == _size.y - 1) ||
               (x == _size.x - 1 && y == 0) || (x == _size.x - 1 && y == _size.y - 1);
    }

    bool IsEdge(int x, int y)
    {
        return x == 0 || x == _size.x - 1 || y == 0 || y == _size.y - 1;
    }

    private void CreateTile(TileBase tile, Vector3Int position, Quaternion rotation)
    {
        _map.SetTile(position, tile);
        var tileObject = _map.GetInstantiatedObject(position);
        tileObject.transform.localScale = new Vector3(_map.cellSize.x, _map.cellSize.z, _map.cellSize.y);
        tileObject.transform.Rotate(rotation.eulerAngles);
    }
}
