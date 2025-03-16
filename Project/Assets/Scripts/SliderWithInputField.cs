using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.TMP_InputField;

public class SliderWithInputField : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_InputField _inputField;

    private void OnEnable() {
        _slider.onValueChanged.AddListener(OnSliderChanged);
        _inputField.onValueChanged.AddListener(OnInputChanged);
        OnSliderChanged(_slider.value);
    }

    private void OnDisable() {
        _slider.onValueChanged.RemoveListener(OnSliderChanged);
        _inputField.onValueChanged.RemoveListener(OnInputChanged);
    }

    private void OnSliderChanged(float value) {
        _inputField.text = value.ToString();
    }

    private void OnInputChanged(string value) {
        if(Convert.ToInt32(_inputField.text) > _slider.maxValue) {
            _inputField.text = _slider.maxValue.ToString();
        } else if(Convert.ToInt32(_inputField.text) < _slider.minValue) {
            _inputField.text = _slider.minValue.ToString();
        }
        _slider.value = Convert.ToInt32(value);
    }
}
