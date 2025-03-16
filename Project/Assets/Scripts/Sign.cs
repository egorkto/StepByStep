using TMPro;
using UnityEngine;

public class SignView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void SetText(string value) {
        _text.text = value;
    }
}
