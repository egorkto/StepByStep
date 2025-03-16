using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionsHandler : IInputSwitcher, IDisposable {
    public event Action<Vector2> Look;
    public event Action<Vector2> Move;
    public event Action Stop;
    public event Action StartSprint;
    public event Action EndSprint;
    public event Action Jump;
    public event Action Interact;
    public event Action Pause;
    public event Action Escape;
    public event Action OpenRules;
    public event Action CloseRules;

    private InputActions _actions;

    public InputActionsHandler() {
        _actions = new InputActions();
        _actions.Player.Look.performed += OnLook;
        _actions.Player.Move.performed += OnMove;
        _actions.Player.Move.canceled += OnStop;
        _actions.Player.Jump.performed += OnJump;
        _actions.Player.Sprint.performed += OnStartSprint;
        _actions.Player.Sprint.canceled += OnEndSprint;
        _actions.Player.Interact.performed += OnInteract;
        _actions.Player.Pause.performed += OnPause;
        _actions.Player.Rules.performed += OnStartRules;
        _actions.Player.Rules.canceled += OnCancelRules;
        _actions.UI.Cancel.performed += OnEscape;
    }

    public void Dispose()
    {
        _actions.Player.Look.performed -= OnLook;
        _actions.Player.Move.performed -= OnMove;
        _actions.Player.Move.canceled -= OnStop;
        _actions.Player.Jump.performed -= OnJump;
        _actions.Player.Sprint.performed -= OnStartSprint;
        _actions.Player.Sprint.canceled -= OnEndSprint;
        _actions.Player.Interact.performed -= OnInteract;
        _actions.Player.Pause.performed -= OnPause;
        _actions.UI.Cancel.performed -= OnEscape;
        _actions.UI.Disable();
        _actions.Player.Disable();
    }

    public void SwitchToUI() {
        _actions.Player.Disable();
        _actions.UI.Enable();
        UnlockCursor();
    }

    public void SwitchToGameplay() {
        _actions.UI.Disable();
        _actions.Player.Enable();
        LockCursor();
    }

    private void LockCursor() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCursor() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnLook(InputAction.CallbackContext context) {
        Look?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnMove(InputAction.CallbackContext context) {
        Move?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnStop(InputAction.CallbackContext context) {
        Stop?.Invoke();
    }

    private void OnJump(InputAction.CallbackContext context) {
        Jump?.Invoke();
    }

    private void OnStartSprint(InputAction.CallbackContext context) {
        StartSprint?.Invoke();
    }

    private void OnEndSprint(InputAction.CallbackContext context) {
        EndSprint?.Invoke();
    }

    private void OnInteract(InputAction.CallbackContext context) {
        Interact?.Invoke();
    }

    private void OnPause(InputAction.CallbackContext context) {
        Pause?.Invoke();
    }

    private void OnStartRules(InputAction.CallbackContext context) {
        OpenRules?.Invoke();
    }

    private void OnCancelRules(InputAction.CallbackContext context) {
        CloseRules?.Invoke();
    }

    private void OnEscape(InputAction.CallbackContext context) {
        Escape?.Invoke();
    }
}