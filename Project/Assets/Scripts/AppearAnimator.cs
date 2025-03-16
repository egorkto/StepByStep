using UnityEngine;

public class AppearAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnEnable() {
        _animator.SetTrigger("Appear");
    }

    public void StartDisable() {
        _animator.SetTrigger("Disappear");
    }

    public void Disable() {
        gameObject.SetActive(false);
    }
}
