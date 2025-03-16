using System.Collections.Generic;
using UnityEngine;

public class LevelStatesGenerator {
    private ISettingsConfirmer _confirmer;
    private IMusicTuner _musicTuner;
    private IInputSwitcher _inputSwitcher;
    private UIWindowsPresenter _windowsPresenter;
    private FinishLevelEffector _effector;
    private Timer _timer;

    public LevelStatesGenerator(ISettingsConfirmer confirmer, IMusicTuner musicTuner, IInputSwitcher inputSwitcher, FinishLevelEffector effector, UIWindowsPresenter windowsPresenter, Timer timer) {
        _confirmer = confirmer;
        _musicTuner = musicTuner;
        _inputSwitcher = inputSwitcher;
        _effector = effector;
        _windowsPresenter = windowsPresenter;
        _timer = timer;
    }

    public List<LevelState> GenerateStates(LevelStateMachine machine) {
        return new List<LevelState>() {
            new GameplayState(machine),
            new PauseState(machine, _confirmer, _musicTuner, _inputSwitcher, _windowsPresenter),
            new LoseLevelState(machine, _effector, _inputSwitcher, _windowsPresenter, _timer),
            new WinLevelState(machine, _effector, _inputSwitcher, _windowsPresenter, _timer)
        };
    }
}