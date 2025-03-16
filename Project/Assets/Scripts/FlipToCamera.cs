using UnityEngine;

public class FlipToCamera : MonoBehaviour {
    private Camera _camera;
    private bool _flipped;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector3 cameraToObject = transform.position - _camera.transform.position;
        float dotProduct = Vector3.Dot(cameraToObject, transform.forward);
        if(dotProduct >= 0 && !_flipped || dotProduct < 0 && _flipped) {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _flipped = !_flipped;
        }
    }
}