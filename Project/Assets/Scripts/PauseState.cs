using UnityEngine;

public class PauseState : LevelState {
    private ISettingsConfirmer _confirmer;
    private IMusicTuner _musicTuner;
    private IInputSwitcher _inputSwitcher;
    private UIWindowsPresenter _windowsPresenter;

    public PauseState(LevelStateMachine machine, ISettingsConfirmer confirmer, IMusicTuner musicTuner, IInputSwitcher inputSwitcher, UIWindowsPresenter windowsPresenter) : base(machine) {
        _confirmer = confirmer;
        _musicTuner = musicTuner;
        _inputSwitcher = inputSwitcher;
        _windowsPresenter = windowsPresenter;
    }

    public override void Enter()
    {
        Time.timeScale = 0;
        _musicTuner.PauseMusic();
        _inputSwitcher.SwitchToUI();
        _windowsPresenter.ShowPause();
    }

    public override void Exit()
    {
        Time.timeScale = 1;
        _musicTuner.UnpauseMusic();
        _inputSwitcher.SwitchToGameplay();
        _windowsPresenter.HidePause();
    }

    public override void OnUnpause()
    {
        if(_confirmer.Confirmed) {
            Machine.SwitchState<GameplayState>();
        } else {
            _windowsPresenter.ShowSettingsConfirmWindow();
        }
    }

    public override void OnLose()
    {
        Machine.SwitchState<LoseLevelState>();
    }

    public override void OnWin()
    {
        Machine.SwitchState<WinLevelState>();
    }
}