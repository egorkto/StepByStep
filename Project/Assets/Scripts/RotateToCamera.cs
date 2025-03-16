using UnityEngine;

public class RotateToCamera : MonoBehaviour {
    [SerializeField] private float _maxDegrees;

    private Camera _camera;
    private Quaternion _lookRotation;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update() {
        _lookRotation = Quaternion.LookRotation(_camera.transform.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _lookRotation, _maxDegrees);
    }
}