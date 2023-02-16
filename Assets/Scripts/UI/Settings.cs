using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioChannel _musicChannel;
    [SerializeField] private Slider _musicAudioLevel;
    [SerializeField] private GameObject _settingsPanel;

    private void Start()
    {
        _musicAudioLevel.value = _musicChannel.Volume;
        _musicAudioLevel.onValueChanged.AddListener(SetMusicVolume);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) ToggleSettings();
    }

    private void SetMusicVolume(float value)
    {
        _musicChannel.Volume = value;
    }

    public void ToggleSettings()
    {
        bool active = !_settingsPanel.activeSelf;
        Time.timeScale = active ? 0 : 1;
        _settingsPanel.SetActive(active);
    }
}
