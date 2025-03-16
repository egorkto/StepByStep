using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguagesDropdown : MonoBehaviour {
    [SerializeField] private TMP_Dropdown _dropdown;

    private int _startValue = 0;

    private void Awake() {
        var options = new List<TMP_Dropdown.OptionData>();
        foreach(var locale in LocalizationSettings.AvailableLocales.Locales) {
            var name = locale.Identifier.CultureInfo.NativeName[0].ToString().ToUpper() + locale.Identifier.CultureInfo.NativeName.Substring(1);
            options.Add(new TMP_Dropdown.OptionData(name));
        }
        _dropdown.ClearOptions();
        _dropdown.AddOptions(options);
    }

    private void Start() {
        _dropdown.value = _startValue;
    }

    public int GetValue() {
        return _dropdown.value;
    }

    public void SetValue(int language) {
        _startValue = language;
        _dropdown.value = language;
    }
}