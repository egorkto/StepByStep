using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScenesNavigator : IDisposable {
    private UIInputHandler _inputHandler;

    public LevelScenesNavigator(UIInputHandler inputHandler) {
        _inputHandler = inputHandler;
        _inputHandler.Restart += OnRestart;
        _inputHandler.ToMenu += OnToMenu;
        _inputHandler.ExitGame += OnExitGame;
    }

    public void Dispose()
    {
        _inputHandler.Restart -= OnRestart;
        _inputHandler.ToMenu -= OnToMenu;
        _inputHandler.ExitGame -= OnExitGame;
    }

    private void OnRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnToMenu() {
        SceneManager.LoadScene("Menu");
    }

    private void OnExitGame() {
        Application.Quit();
    }
}