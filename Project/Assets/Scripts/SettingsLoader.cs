using System.IO;
using UnityEngine;

public class SettingsLoader {
    private string _path;

    public SettingsLoader(string path) {
        _path = path;
    }

    public bool TryLoad(out Settings settings)
    {
        if(File.Exists(_path))
        {
            var json = File.ReadAllText(_path);
            settings = JsonUtility.FromJson<Settings>(json);
            return true;
        }
        settings = new Settings();
        return false;
    }
}