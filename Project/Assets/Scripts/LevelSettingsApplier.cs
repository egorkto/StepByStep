using Unity.Cinemachine;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Localization.Settings;
using UnityEngine.Audio;

public class LevelSettingsApplier : ISettingsApplier {
    private AudioSource _musicSource;
    private CinemachineInputAxisController _cinemachineInputAxisController;
    private CinemachineCamera _camera;
    private Vector2 _sensitivitySettingsBounds;
    private AudioMixer _mixer;

    private float[] _startGains;

    public LevelSettingsApplier(AudioMixer mixer, CinemachineInputAxisController inputController, CinemachineCamera camera, Vector2 sensitivitySettingsBounds) {
        _mixer = mixer;
        _cinemachineInputAxisController = inputController;
        _camera = camera;
        _sensitivitySettingsBounds = sensitivitySettingsBounds;
        _startGains = new float[_cinemachineInputAxisController.Controllers.Count];
        for(var i = 0; i < _cinemachineInputAxisController.Controllers.Count; i++) {
            _startGains[i] = _cinemachineInputAxisController.Controllers[i].Input.Gain;
        }
    }

    public void Apply(Settings settings) {
        var remapMusicVolume = math.remap(0, 100, -40, 0, settings.MusicVolume);
        remapMusicVolume = remapMusicVolume <= -40 ? -80f : remapMusicVolume;
        _mixer.SetFloat("MusicVolume", remapMusicVolume);
        var remapEffectsVolume = math.remap(0, 100, -40, 0, settings.EffectsVolume);
        remapEffectsVolume = remapEffectsVolume <= -40 ? -80f : remapEffectsVolume;
        _mixer.SetFloat("EffectsVolume", remapEffectsVolume);
        _camera.Lens.FieldOfView = settings.FOV;
        for(var i = 0; i < _cinemachineInputAxisController.Controllers.Count; i++) {
            var gain = _startGains[i] * math.remap(_sensitivitySettingsBounds.x, _sensitivitySettingsBounds.y, 0.1f, 5f, settings.Sensitivity);
            _cinemachineInputAxisController.Controllers[i].Input.Gain = gain;
        }
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[settings.Language];
    }
}