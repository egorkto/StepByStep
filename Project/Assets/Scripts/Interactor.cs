using UnityEngine;

public class Interactor
{
    private InputActionsHandler _handler;
    private Camera _camera;
    private float _interactDistance; 
    private IInteractable _currentInteractable;

    public Interactor(InputActionsHandler handler, Camera camera, float interactDistance) {
        _handler = handler;
        _camera = camera;
        _interactDistance = interactDistance;
        _handler.Interact += OnInteract;
    }

    ~Interactor() {
        _handler.Interact -= OnInteract;
    }

    public void Update() {
        if(Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, _interactDistance)) {
            if(hit.transform.gameObject.TryGetComponent(out IInteractable interactable)) {
                if(_currentInteractable != interactable) {
                    if(_currentInteractable != null) {
                        _currentInteractable.SetActive(false);  
                    }
                    _currentInteractable = interactable;
                    _currentInteractable.SetActive(true);
                }
                return;
            }
        }
        if(_currentInteractable != null) {
            _currentInteractable.SetActive(false);
            _currentInteractable = null;    
        }
    }

    private void OnInteract() {
        if(_currentInteractable != null) {
            _currentInteractable.Interact();
            return;
        }
    }
}   
