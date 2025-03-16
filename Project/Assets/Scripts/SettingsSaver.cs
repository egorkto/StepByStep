using System.IO;
using UnityEngine;

public class SettingsSaver {
    public readonly string Path;

    public SettingsSaver(string fileName) {
        Path = Application.persistentDataPath + "/" + fileName + ".json";
    }

    public void Save(Settings data)
    {
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Path, json);
    }
}