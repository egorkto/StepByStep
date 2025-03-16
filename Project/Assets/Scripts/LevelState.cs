public abstract class LevelState {
    public readonly LevelStateMachine Machine;

    public LevelState(LevelStateMachine machine) {
        Machine = machine;
    }

    public virtual void Enter() {

    }

    public virtual void Exit() {

    }

    public virtual void OnPause() {

    }

    public virtual void OnUnpause() {

    }

    public virtual void OnLose() {

    }

    public virtual void OnWin() {
        
    }
}