using UnityEngine;

public class Mover {

    private Vector3 _planeMove;
    private Vector2 _moveInput;
    private float _yMove;
    private InputActionsHandler _handler;
    private CharacterController _controller;
    private PlayerStats _stats;
    private float _currentMoveSpeed;

    public Mover(InputActionsHandler handler, CharacterController controller, PlayerStats stats) {
        _controller = controller;
        _stats = stats;
        _currentMoveSpeed = _stats.WalkSpeed;
        _handler = handler;
        _handler.Jump += OnJump;
        _handler.Move += OnMove;
        _handler.Stop += OnStop;
        _handler.StartSprint += OnStartSprint;
        _handler.EndSprint += OnEndSprint;
    }

    ~Mover() {
        _handler.Jump -= OnJump;
        _handler.Move -= OnMove;
        _handler.Stop += OnStop;
        _handler.StartSprint -= OnStartSprint;
        _handler.EndSprint -= OnEndSprint;
    }

    public void Update() {
        if(_controller.isGrounded && _yMove < 0.0f) {
            _yMove = 0;
        }
        _yMove -= _stats.GravityAcceleration * Time.fixedDeltaTime;
        _planeMove = Camera.main.transform.TransformDirection(new Vector3(_moveInput.x, 0, _moveInput.y));
        _planeMove.y = 0;
        _planeMove.Normalize();
        _planeMove *= _currentMoveSpeed;
        _controller.Move(new Vector3(_planeMove.x, _yMove, _planeMove.z) * Time.fixedDeltaTime);
    }

    private void OnMove(Vector2 vector) {
        _moveInput = vector;
    }

    private void OnStop() {
        _moveInput = Vector2.zero;
    }

    private void OnStartSprint() {
        _currentMoveSpeed = _stats.SprintSpeed;
    }

    private void OnEndSprint() {
        _currentMoveSpeed = _stats.WalkSpeed;
    }

    private void OnJump() {
        if(_controller.isGrounded) {
            _yMove = _stats.JumpForce;
        }
    }
}