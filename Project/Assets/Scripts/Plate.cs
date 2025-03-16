using System;
using UnityEngine;

public class Plate : MonoBehaviour {
    public event Action PlayerTriggered;

    public PlateType Type => _type;
    [SerializeField] private PlateType _type;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.TryGetComponent(out Player player)) {
            PlayerTriggered?.Invoke();
            ProcessPlayerTriggerEnter();
        }
    }

    protected virtual void ProcessPlayerTriggerEnter() {}
}

public enum PlateType {
    None = 0,
    Solid,
    Transparent
}