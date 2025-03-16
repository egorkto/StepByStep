using UnityEngine;

public class FirstPersonController
{
    private Mover _mover;
    private Interactor _interactor;

    public FirstPersonController(InputActionsHandler handler, CharacterController controller, Camera camera, float interactDistance, PlayerStats stats) {
        _mover = new Mover(handler, controller, stats);    
        _interactor = new Interactor(handler, camera,  interactDistance);
    }

    public void FixedUpdate() {
        _mover.Update();
        _interactor.Update();
    }
}