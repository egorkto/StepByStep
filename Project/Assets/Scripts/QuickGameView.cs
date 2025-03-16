using UnityEngine.UI;

public class QuickGameView {
    private Slider _rowsSlider;
    private Slider _columnsSlider;
    private Slider _upperHintsIntervalSlider;

    public QuickGameView(Slider rowsSlider, Slider columnsSlider, Slider upperHintsIntervalSlider) {
        _rowsSlider = rowsSlider;
        _columnsSlider = columnsSlider;
        _upperHintsIntervalSlider = upperHintsIntervalSlider;
    }

    public void SetDataTo(LevelConfig config) {
        config.SetRowsCount((int)_rowsSlider.value);
        config.SetColumnsCount((int)_columnsSlider.value);
        config.SetUpperHintsInterval((int)_upperHintsIntervalSlider.value);
    }
}