using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValuePresenter : MonoBehaviour
{
    protected TMP_Text Text => _text;

    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;

    private void OnEnable() {
        _slider.onValueChanged.AddListener(OnChanged);
        OnChanged(_slider.value);
    }

    private void OnDisable() {
        _slider.onValueChanged.RemoveListener(OnChanged);
    }

    protected virtual void OnChanged(float value) {
        _text.text = value.ToString();
    }
}
