using UnityEngine.UI;

public class SettingsView : ISettingsConfirmer
{
    public bool Confirmed => _currentSettings == GetSettings();

    private RadialSlider _musicSlider;
    private RadialSlider _effectsSlider;
    private Slider _sensitivitySlider;
    private Slider _FOVSlider;
    private LanguagesDropdown _languagesDropdown;

    private Settings _currentSettings;

    public SettingsView(RadialSlider musicSlider, RadialSlider effectsSlider, Slider sensitivitySlider, Slider fovSlider, LanguagesDropdown languagesDropdown) {
        _musicSlider = musicSlider;
        _effectsSlider = effectsSlider;
        _sensitivitySlider = sensitivitySlider;
        _FOVSlider = fovSlider;
        _languagesDropdown = languagesDropdown;
    }

    public Settings GetSettings() {
        var data = new Settings() {
            MusicVolume = _musicSlider.Value,
            EffectsVolume = _effectsSlider.Value,
            Sensitivity = _sensitivitySlider.value,
            FOV = (int)_FOVSlider.value,
            Language = _languagesDropdown.GetValue()
        };
        return data;
    }

    public void SetSettings(Settings settings) {
        _musicSlider.SetValue(settings.MusicVolume);
        _effectsSlider.SetValue(settings.EffectsVolume);
        _sensitivitySlider.value = settings.Sensitivity;
        _FOVSlider.value = settings.FOV;
        _languagesDropdown.SetValue(settings.Language);
    }

    public void ConfirmSettings() {
        _currentSettings = GetSettings();
    }

    public void ResetSettings() {
        SetSettings(_currentSettings);
    }
}
