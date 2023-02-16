using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Settings", menuName = "Scriptable Objects/Triception TD/Player Settings")]
public class PlayerSettings : ScriptableObjectSingleton<PlayerSettings>
{
    private string _filePath => Path.Combine(Application.dataPath, "PlayerSettings.json");
    [SerializeField] private AudioChannel _musicAudioChannel;

    [SerializeField] private float _musicVolume;
    public float MusicVolume {
        get {
            return _musicVolume;
        }
        set {
            _musicVolume = Mathf.Clamp01(value);
            _musicAudioChannel.Volume = _musicVolume;
        }
    }

    public void LoadData()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            Data data = JsonUtility.FromJson<Data>(json);

            MusicVolume = data.MusicVolume;
        }
        else
        {
            SaveData();
        }
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(new Data()
        {
            MusicVolume = MusicVolume,
        }, true);

        File.WriteAllText(_filePath, data);
    }

    private struct Data
    {
        public float MusicVolume;
    }
}
