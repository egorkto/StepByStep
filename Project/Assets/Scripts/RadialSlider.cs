using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadialSlider : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public int Value => _value;

    [SerializeField] private TMP_Text _valueText;
    [SerializeField] private string _postFix;
    [SerializeField] private Image _fillingImage;

    private int _value = 100;
    private float _angle;

    public void SetValue(int value) {
        _value = value;
        _valueText.text = _value.ToString() + _postFix;
        _fillingImage.fillAmount = _value / 100.0f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _angle = GetMouseAngle();
        SetValue((int)((1 - _angle / 360.0f) * 100));
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if(_angle <= 10 && Input.mousePosition.x < _fillingImage.rectTransform.position.x && Input.mousePosition.y < _fillingImage.rectTransform.position.y) {
            _angle = 0;
        }
        else if(_angle >= 350 && Input.mousePosition.x >= _fillingImage.rectTransform.position.x && Input.mousePosition.y < _fillingImage.rectTransform.position.y) {
            _angle = 360;
        }
        else {
            _angle = GetMouseAngle();
        }
        SetValue((int)((1 - _angle / 360.0f) * 100));
    }

    private float GetMouseAngle() {
        var angle = Vector3.Angle(-Vector3.up, Input.mousePosition - _fillingImage.rectTransform.position);
        if(Input.mousePosition.x < _fillingImage.rectTransform.position.x) {
            angle = 360 - angle;
        }
        return angle;
    }
}
