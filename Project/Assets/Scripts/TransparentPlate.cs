using UnityEngine;

public class TransparentPlate : Plate {
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private ParticleSystem _disappearParticles;

    protected override void ProcessPlayerTriggerEnter()
    {
        var pariclesRenderer = _disappearParticles.GetComponent<ParticleSystemRenderer>();
        var color = _renderer.materials[0].color;
        pariclesRenderer.materials[0].SetColor("_EmissionColor", color);
        pariclesRenderer.materials[0].color = color;
        _disappearParticles.Clear();
        _disappearParticles.Play();
        gameObject.SetActive(false);
    }
}