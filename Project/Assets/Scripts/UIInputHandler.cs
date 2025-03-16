using System;
using UnityEngine;

public class UIInputHandler : MonoBehaviour
{
    public event Action Continue;
    public event Action ConfirmSettings;
    public event Action CancelSettings;
    public event Action Restart;
    public event Action ToMenu;
    public event Action QuickGameStarted;
    public event Action ExitGame;

    public void OnContinue() {
        Continue?.Invoke();
    }

    public void OnConfirmSettings() {

        ConfirmSettings?.Invoke();
    }

    public void OnCancelSettings() {
        CancelSettings?.Invoke();
    }

    public void OnRestart() {
        Restart?.Invoke();
    }

    public void OnToMenu() {
        ToMenu?.Invoke();
    }

    public void OnStartQuickGame() {
        QuickGameStarted?.Invoke();
    }

    public void OnExitGame() {
        ExitGame?.Invoke();
    }
}
