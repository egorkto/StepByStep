using System;
using UnityEngine.SceneManagement;
using Application = UnityEngine.Application;

public class MenuScenesNavigator : IDisposable {
    private UIInputHandler _inputHandler;
    private LevelConfigurator _configurator;

    public MenuScenesNavigator(UIInputHandler inputHandler, LevelConfigurator configurator) {
        _inputHandler = inputHandler;
        _configurator = configurator;
        _inputHandler.QuickGameStarted += OnStartQuickGame;
        _inputHandler.ExitGame += OnExitGame;
    }

    public void Dispose()
    {
        _inputHandler.QuickGameStarted -= OnStartQuickGame;
        _inputHandler.ExitGame -= OnExitGame;
    }

    private void OnStartQuickGame() {
        _configurator.ConfigurateQuickGame();
        SceneManager.LoadScene("QuickGame");
    }

    private void OnExitGame() {
        Application.Quit();
    }
}