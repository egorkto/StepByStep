using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Mood/LevelConfig", order = 0)]
public class LevelConfig : ScriptableObject {
    public int PlateRowsCount => _plateRowsCount;
    public int PlateColumnsCount => _plateColumnsCount;
    public int UpperHintsInterval => _upperHintsInterval;
    public int SaveZoneWidth => _saveZoneWidth;
    public int BordersOffset => _bordersOffset; 

    [SerializeField] private int _plateRowsCount;
    [SerializeField] private int _plateColumnsCount;
    [SerializeField] private int _upperHintsInterval;
    [SerializeField] private int _saveZoneWidth;
    [SerializeField] private int _bordersOffset;

    public void SetRowsCount(int value) {
        _plateRowsCount = value;
    }

    public void SetColumnsCount(int value) {
        _plateColumnsCount = value;
    }

    public void SetUpperHintsInterval(int value) {
        _upperHintsInterval = value;
    }
}