using UnityEngine;

public class PlateView : MonoBehaviour, IInteractable
{
    public bool Selected => _selected;
    [ColorUsage(true,true)] public Color Color => _material.color;

    [SerializeField] private Plate _plate;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Color _markedColor;
    [SerializeField] private ParticleSystem _selectParticles;
    [SerializeField] private AudioSource _markSource;
    [SerializeField] private AudioSource _landSource;

    private bool _selected;
    private Material _material;
    [ColorUsage(true,true)] private Color _defaultColor;
    private bool _isMarked = false;

    public void SetActive(bool value) {
        if(value) {
            _selectParticles.Play();
        } else {
            _selectParticles.Stop();
        }
    }

    public void Interact() {
        _isMarked = !_isMarked;
        _material.color = _isMarked ? _markedColor : _defaultColor;
        if(_isMarked) {
            _markSource.Play();
        }
    }

    private void Start() {
        _material = _renderer.materials[0];
        _defaultColor = _material.color;
    }

    private void OnEnable() {
        _plate.PlayerTriggered += OnTriggered;
    }

    private void OnDisable() {
        _plate.PlayerTriggered -= OnTriggered;
    }

    private void OnTriggered() {
        _landSource.Play();
    }
}
