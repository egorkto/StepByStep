using UnityEngine.SceneManagement;

public class LevelConfigurator {
    private QuickGameView _quickGameView;
    private LevelConfig _config;

    public LevelConfigurator(QuickGameView quickGameView, LevelConfig levelConfig) {
        _quickGameView = quickGameView;
        _config = levelConfig;
    }

    public void ConfigurateQuickGame() {
        _quickGameView.SetDataTo(_config);
    }
}   