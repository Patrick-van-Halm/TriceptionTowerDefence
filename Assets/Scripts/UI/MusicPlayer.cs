using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private Sprite _playSprite;
    [SerializeField] private Sprite _pauseSprite;
    [SerializeField] private Image _playPauseButtonImage;
    [SerializeField] private TMP_Text _currentSongText;
    [SerializeField] private EmptyEvent _currentSongChanged;

    private void OnEnable()
    {
        if (!MusicPlayerManager.Instance) return;
        if (!MusicPlayerManager.Instance.IsPlayingMusic) return;
        UpdateCurrentSongName();
        UpdatePlayPauseSprite();
    }

    private void Awake()
    {
        _currentSongChanged.RegisterAction(UpdateCurrentSongName);
    }

    private void UpdateCurrentSongName()
    {
        _currentSongText.text = MusicPlayerManager.Instance.SongName;
        UpdatePlayPauseSprite();
    }

    public void NextSong()
    {
        MusicPlayerManager.Instance.NextSong();
        UpdatePlayPauseSprite();
    }

    public void PrevSong()
    {
        MusicPlayerManager.Instance.PrevSong();
        UpdatePlayPauseSprite();
    }

    public void PlayPause()
    {
        MusicPlayerManager.Instance.PlayPause();
        UpdatePlayPauseSprite();
    }

    private void UpdatePlayPauseSprite()
    {
        if (!MusicPlayerManager.Instance.IsPaused)
            _playPauseButtonImage.sprite = _pauseSprite;
        else
            _playPauseButtonImage.sprite = _playSprite;
    }
}
