using UnityEngine;

public class LoseLevelState : LevelState {
    private FinishLevelEffector _effector;
    private IInputSwitcher _inputSwitcher;
    private UIWindowsPresenter _windowsPresenter;
    private Timer _timer;

    public LoseLevelState(LevelStateMachine machine, FinishLevelEffector effector, IInputSwitcher inputSwitcher, UIWindowsPresenter windowsPresenter, Timer timer) : base(machine) {
        _inputSwitcher = inputSwitcher;
        _effector = effector;
        _windowsPresenter = windowsPresenter;
        _timer = timer;
    }

    public override void Enter()
    {
        _effector.EffectLose();
        _windowsPresenter.ShowLoseWindow();
        _inputSwitcher.SwitchToUI();
        _timer.Disable();
    }
}