using System;

public class PlayerTracker : IDisposable {
    public event Action Lose;
    public event Action Win;

    private PlayerTrigger _loseTrigger;
    private PlayerTrigger _winTrigger;

    public PlayerTracker(ITriggersHolder triggersHolder) {
        _loseTrigger = triggersHolder.GetLoseTrigger();
        _winTrigger = triggersHolder.GetWinTrigger();
        _loseTrigger.PlayerEntered += OnLose;
        _winTrigger.PlayerEntered += OnWin;
    }

    public void Dispose()
    {
        _loseTrigger.PlayerEntered -= OnLose;
        _winTrigger.PlayerEntered -= OnWin;
    }

    private void OnLose() {
        Lose?.Invoke();
    }

    private void OnWin() {
        Win?.Invoke();
    }
}