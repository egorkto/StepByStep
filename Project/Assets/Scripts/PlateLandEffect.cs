using UnityEngine;

public class PlateLandEffect : MonoBehaviour {
    [SerializeField] private CharacterController _controller;
    [SerializeField] private ParticleSystem _landParticles;

    private void OnCollisionEnter() {
        Debug.Log("col");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.isTrigger && other.gameObject.TryGetComponent(out PlateView view) && _controller.velocity.y <= 0) {
            var renderer = _landParticles.GetComponent<ParticleSystemRenderer>();
            renderer.materials[0].SetColor("_EmissionColor", view.Color);
            renderer.materials[0].color = view.Color;
            _landParticles.Clear();
            _landParticles.Play();
        }
    }
}