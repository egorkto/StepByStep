using UnityEngine;
using Random = System.Random;

public class PlatesMapGenerator {
    private Random _random;

    public PlatesMapGenerator() {
        _random = new Random();
    }

    public PlateType[,] GenerateMap(Vector2Int size) {
        var map = new PlateType[size.x, size.y];
        for(int y = 0; y < map.GetLength(1); y++) {
            GenerateRequired(map, y);
            for(int x = 0; x < map.GetLength(0); x++) {
                if(map[x,y] == PlateType.None) {
                    var randomType = _random.Next(0, 2) == 0 ? PlateType.Transparent : PlateType.Solid;
                    if(CanPlace(randomType, map, x, y)) {
                        map[x,y] = randomType;
                    } else {
                        map[x, y] = GetOpposite(randomType);
                    }
                }
            }
        }
        return map;
    }

    private void GenerateRequired(PlateType[,] map, int y) {
        for(int x = 0; x < map.GetLength(0); x++) {
            if(Require(PlateType.Transparent, map, x, y)) {
                map[x, y] = PlateType.Transparent;
            } else if(Require(PlateType.Solid, map, x, y)) {
                map[x, y] = PlateType.Solid;
            }
        }
    }

    private bool Require(PlateType type, PlateType[,] map, int x, int y) {
        var opposite =  GetOpposite(type);
        var isTransparentAfterSolid = type == PlateType.Transparent && y > 0 && map[x, y-1] == PlateType.Solid;
        var isNeighbours = x > 0 && x < map.GetLength(0) - 1 && map[x+1,y] != PlateType.None && map[x+1,y] == opposite && 
                           map[x-1,y] != PlateType.None && map[x-1,y] == opposite;
        var twoRight = x >= 2 && map[x-1,y] != PlateType.None && map[x-1,y] == opposite && 
                       map[x-2,y] != PlateType.None && map[x-2,y] == opposite;
        var twoLeft = x < map.GetLength(0) - 2 && map[x+1,y] != PlateType.None && map[x+1,y] == opposite && 
                      map[x+2,y] != PlateType.None && map[x+2,y] == opposite;
        return isTransparentAfterSolid || isNeighbours || twoLeft || twoRight;
    }

    private bool CanPlace(PlateType type, PlateType[,] map, int x, int y) {
        var isNeighbours = x > 0 && x < map.GetLength(0) - 1 && map[x+1,y] != PlateType.None && map[x+1,y] == type && 
                           map[x-1,y] != PlateType.None && map[x-1,y] == type;
        var twoRight = x >= 2 && map[x-1,y] != PlateType.None && map[x-1,y] == type && 
                       map[x-2,y] != PlateType.None && map[x-2,y] == type;
        var twoLeft = x < map.GetLength(0) - 2 && map[x+1,y] != PlateType.None && map[x+1,y] == type && 
                      map[x+2,y] != PlateType.None && map[x+2,y] == type;
        return !isNeighbours && !twoLeft && !twoRight;
    }

    private PlateType GetOpposite(PlateType type) {
        switch(type) {
            case PlateType.Solid:
                return PlateType.Transparent;
            case PlateType.Transparent:
                return PlateType.Solid;
            default:
                return PlateType.None;
        }
    }
}