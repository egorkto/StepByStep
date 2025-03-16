using UnityEngine;

public class UIWindowsPresenter {
    private GameObject _pauseWindow;
    private GameObject _settingsConfirmWindow;
    private GameObject _loseWindow;
    private GameObject _winWindow;
    private GameObject _rulesWindow;

    public UIWindowsPresenter(GameObject pauseWindow, GameObject settingsConfirmWindow, GameObject loseWindow, GameObject winWindow, GameObject rulesWindow) {
        _pauseWindow = pauseWindow;
        _settingsConfirmWindow = settingsConfirmWindow;
        _loseWindow = loseWindow;
        _winWindow = winWindow;
        _rulesWindow = rulesWindow;
    }

    public void ShowLoseWindow() {
        _loseWindow.SetActive(true);
    }

    public void ShowWinWindow() {
        _winWindow.SetActive(true);
    }

    public void ShowSettingsConfirmWindow() {
        _settingsConfirmWindow.SetActive(true);
    }

    public void ShowPause() {
        _pauseWindow.SetActive(true);
    }

    public void HidePause() {
        _pauseWindow.SetActive(false);
    }

    public void ShowRules() {
        _rulesWindow.SetActive(true);
    }

    public void HideRules() {
        _rulesWindow.SetActive(false);
    }
}