public class GameplayState : LevelState {
    public GameplayState(LevelStateMachine machine) : base(machine) {
        
    }

    public override void OnPause()
    {
        Machine.SwitchState<PauseState>();
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