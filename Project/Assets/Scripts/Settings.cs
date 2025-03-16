using System;
using Unity.VisualScripting;

[Serializable]
public struct Settings {
    public int MusicVolume;
    public int EffectsVolume;
    public float Sensitivity;
    public int FOV;
    public int Language;

    public Settings(int musicVolume = 100, int effectsVolume = 100, float sensevity = 0, int fov = 80, int language = 0) {
        MusicVolume = musicVolume;
        EffectsVolume = effectsVolume;
        Sensitivity = sensevity;
        FOV = fov;
        Language = language;
    }

    public override string ToString()
    {
        return "Music: " + MusicVolume + " Effects: " + EffectsVolume + " Sensitivity: " + Sensitivity + " FOV: " + FOV + " Language: " + Language.ToString();
    }

    public static bool operator ==(Settings a, Settings b) {
        return a.MusicVolume == b.MusicVolume && a.EffectsVolume == b.EffectsVolume && a.Sensitivity == b.Sensitivity && a.FOV == b.FOV && a.Language == b.Language;
    }

    public static bool operator !=(Settings a, Settings b) {
        return !(a.MusicVolume == b.MusicVolume && a.EffectsVolume == b.EffectsVolume && a.Sensitivity == b.Sensitivity && a.FOV == b.FOV && a.Language == b.Language);
    }
}