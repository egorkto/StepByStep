using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatesFieldSpawner
{
    private Tilemap _platesTilemap;
    private Tilemap _signsTilemap;
    private TileBase _solidPlateTile;
    private TileBase _transparentPlateTile;
    private TileBase _sideSignTile;
    private TileBase _upperSignTile;
    private PlatesMapGenerator _generator;

    public PlatesFieldSpawner(Tilemap platesTilemap, Tilemap signsTilemap, TileBase solidPlate, TileBase transparentPlate, TileBase sideSign, TileBase upperSign) 
    {
        _platesTilemap = platesTilemap;
        _signsTilemap = signsTilemap;
        _solidPlateTile = solidPlate;
        _transparentPlateTile = transparentPlate;
        _sideSignTile = sideSign;
        _upperSignTile = upperSign;
        _generator = new PlatesMapGenerator();
    }

    public void SpawnPlates(Vector2Int fieldSize, int platesBetweenUpperSigns, ref Vector3Int position) {
        var map = _generator.GenerateMap(fieldSize);
        var startPosition = position;
        int transparentsInRow;
        for(int y = 0; y < map.GetLength(1); y++) {
            transparentsInRow = GetTransparentsInRow(map, y);
            SpawnSign(position, _sideSignTile, transparentsInRow);
            position.x++;
            for(int x = 0; x < map.GetLength(0); x++) {
                SpawnPlate(map[x,y], position);
                if(y == 0 && map[x,y] == PlateType.Transparent) {
                    _platesTilemap.GetInstantiatedObject(position).SetActive(false);
                }
                if(CanSpawnUpperSign(platesBetweenUpperSigns, y)) {
                    SpawnSign(position, _upperSignTile, GetTransparentsFront(map, new Vector2Int(x,y), platesBetweenUpperSigns));
                }
                position.x++;
            }
            SpawnSign(position, _sideSignTile, transparentsInRow);
            position.y++;
            position.x = startPosition.x;
        }
    }

    private bool CanSpawnUpperSign(int interval, int currentRow) {
        return interval > 0 && currentRow % interval == 0;
    }

    private int GetTransparentsInRow(PlateType[,] map, int row) {
        return Enumerable.Range(0, map.GetLength(0)).Select(column => map[column, row]).Where(type => type == PlateType.Transparent).Count();
    }

    private int GetTransparentsFront(PlateType[,] map, Vector2Int currentPosition, int lenght) {
        var count = currentPosition.y + lenght <= map.GetLength(1) ? lenght : map.GetLength(1) - currentPosition.y;
        return Enumerable.Range(currentPosition.y, count).Select(row => map[currentPosition.x, row]).Where(type => type == PlateType.Transparent).Count();
    }

    private void SpawnPlate(PlateType type, Vector3Int position) {
        switch(type) {
            case PlateType.Solid:
                _platesTilemap.SetTile(position, _solidPlateTile);
                break;
            case PlateType.Transparent:
                _platesTilemap.SetTile(position, _transparentPlateTile);
                break;
            default:
                Debug.LogError("Cant spawn none plate");
                break;
        }
    }

    private void SpawnSign(Vector3Int position, TileBase tileBase, int number) {
        _signsTilemap.SetTile(position, tileBase);
        var sign = _signsTilemap.GetInstantiatedObject(position).GetComponent<SignView>();
        sign.SetText(number.ToString());
    }
}