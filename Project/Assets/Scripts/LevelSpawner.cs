using UnityEngine;

public class LevelSpawner : ITriggersHolder {
    private SaveZoneSpawner _saveZoneSpawner;
    private PlatesFieldSpawner _platesFieldSpawner;
    private GameObject _levelBorders;
    private Grid _grid;
    private CharacterController _player;

    private GameObject _loseTriggerPrefab;
    private PlayerTrigger _winTrigger;
    private PlayerTrigger _loseTrigger;
    
    public LevelSpawner(Grid grid, SaveZoneSpawner zoneSpawner, PlatesFieldSpawner platesFieldSpawner, GameObject borders, PlayerTrigger loseTriggerPrefab, CharacterController player) {
        _saveZoneSpawner = zoneSpawner;
        _platesFieldSpawner = platesFieldSpawner;
        _levelBorders = borders;
        _grid = grid;
        _loseTriggerPrefab = loseTriggerPrefab.gameObject;
        _player = player;
    }

    public void SpawnLevel(LevelConfig config) {
        var fieldPlatesLenght = config.PlateColumnsCount + 2;
        var fieldPlatesWidth = config.SaveZoneWidth * 2 + config.PlateRowsCount + 1;
        var saveZoneSize = new Vector2Int(fieldPlatesLenght, config.SaveZoneWidth);
        var position = new Vector3Int(0, 0, 0);

        var borderWidth = (_grid.cellSize.x * (fieldPlatesLenght > fieldPlatesWidth ? fieldPlatesLenght : fieldPlatesWidth) + config.BordersOffset * 2) / 10;
        _levelBorders.transform.localScale = Vector3.one * borderWidth;
        _levelBorders.transform.position = new Vector3(_grid.cellSize.x * fieldPlatesWidth / 2, 0, _grid.cellSize.y * fieldPlatesLenght / 2) + position;
        _loseTrigger = GameObject.Instantiate(_loseTriggerPrefab, _levelBorders.transform, false).GetComponent<PlayerTrigger>();
        _loseTrigger.transform.position = _loseTrigger.transform.position - Vector3.up * 10f;
        _loseTrigger.transform.localScale = new Vector3(_levelBorders.transform.localScale.x, _loseTrigger.transform.localScale.y, _levelBorders.transform.localScale.z);

        var startOrigin = _saveZoneSpawner.SpawnPlatformWithOrigin(saveZoneSize, ref position);
        startOrigin.Disactivate();
        var playerPosition = new Vector3(startOrigin.transform.position.x, startOrigin.transform.position.y + _player.transform.localScale.y / 2, startOrigin.transform.position.z);
        _player.enabled = false;
        _player.transform.position = playerPosition;
        _player.enabled = true;

        _platesFieldSpawner.SpawnPlates(new Vector2Int(config.PlateColumnsCount, config.PlateRowsCount), config.UpperHintsInterval, ref position);

        position.y++;
        var finishOrigin = _saveZoneSpawner.SpawnPlatformWithOrigin(saveZoneSize, ref position);
        finishOrigin.Activate();
        _winTrigger = finishOrigin.Trigger;
    }

    public PlayerTrigger GetWinTrigger() {
        return _winTrigger;
    }

    public PlayerTrigger GetLoseTrigger() {
        return _loseTrigger;
    }
}