using UnityEngine;

public class PlatformOrigin : MonoBehaviour {
    public PlayerTrigger Trigger => _playerTrigger;

    [SerializeField] private PlayerTrigger _playerTrigger;
    [SerializeField] private ParticleSystem _particles;

    public void Activate() {
        _playerTrigger.enabled = true;
        _particles.Play();
    }

    public void Disactivate() {
        _playerTrigger.enabled = false;
        _particles.Stop();
    }
}