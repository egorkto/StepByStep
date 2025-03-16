using Unity.Mathematics;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;

public class MenuSettingsApplier : ISettingsApplier {
    private AudioMixer _mixer;

    public MenuSettingsApplier(AudioMixer mixer) {
        _mixer = mixer;
    }

    public void Apply(Settings settings) {
        var remapMusicVolume = math.remap(0, 100, -40, 0, settings.MusicVolume);
        remapMusicVolume = remapMusicVolume <= -40 ? -80f : remapMusicVolume;
        _mixer.SetFloat("MusicVolume", remapMusicVolume);
        var remapEffectsVolume = math.remap(0, 100, -40, 0, settings.EffectsVolume);
        remapEffectsVolume = remapEffectsVolume <= -40 ? -80f : remapEffectsVolume;
        _mixer.SetFloat("EffectsVolume", remapEffectsVolume);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[settings.Language];
    }
}