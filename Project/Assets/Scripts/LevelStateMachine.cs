using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelStateMachine : IDisposable {
    private InputActionsHandler _inputActionsHandler;
    private UIInputHandler _uiInputHandler;
    private PlayerTracker _playerTracker;
    private LevelState _currentState;
    private List<LevelState> _states;

    public LevelStateMachine(LevelStatesGenerator generator, InputActionsHandler actionsHandler, UIInputHandler uiInputHandler, PlayerTracker tracker) {
        _inputActionsHandler = actionsHandler;
        _uiInputHandler = uiInputHandler;
        _playerTracker = tracker;
        _states = generator.GenerateStates(this);
        SwitchState<GameplayState>();
        _inputActionsHandler.SwitchToGameplay();
        _inputActionsHandler.Pause += OnPause;
        _inputActionsHandler.Escape += OnContinue;
        _uiInputHandler.Continue += OnContinue;
        _playerTracker.Lose += OnLose;
        _playerTracker.Win += OnWin;
        Time.timeScale = 1f;
    }

    public void Dispose()
    {
        _inputActionsHandler.Pause -= OnPause;
        _inputActionsHandler.Escape -= OnContinue;
        _uiInputHandler.Continue -= OnContinue;
        _playerTracker.Lose -= OnLose;
        _playerTracker.Win -= OnWin;
        Time.timeScale = 1f;
    }

    private void OnPause() {
        _currentState.OnPause();
    }

    private void OnContinue() {
        _currentState.OnUnpause();
    }

    private void OnLose() {
        _currentState.OnLose();
    }

    private void OnWin() {
        _currentState.OnWin();
    }
    
    public void SwitchState<T>() where T : LevelState {
        if (_states.OfType<T>().Count() == 0)
            Debug.LogError("There is no state with type of: " + typeof(T).Name);
        if(_currentState != null) {
            _currentState.Exit();
        }
        _currentState = _states.Find(s => s is T);
        _currentState.Enter();
    }

}