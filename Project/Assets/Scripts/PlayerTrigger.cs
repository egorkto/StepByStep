using System;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour {
    public event Action PlayerEntered;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.TryGetComponent(out Player player)) {
            PlayerEntered?.Invoke();
        }
    }
}