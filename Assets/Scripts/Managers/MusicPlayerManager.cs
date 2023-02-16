using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerManager : SingletonMonoBehaviour<MusicPlayerManager>
{
    [SerializeField] private AudioReference[] _songs;
    [SerializeField] private EmptyEvent _currentSongChanged;

    private int _songId = -1;
    private AudioManager.AudioInstance _musicInstance;

    public bool IsPaused => _musicInstance != null && _musicInstance.IsPaused();
    public string SongName => _songs[_songId].name;
    public bool IsPlayingMusic => _musicInstance != null;

    private void Start()
    {
        NextSong();
    }

    public void NextSong()
    {
        StopCurrentSongForcefully();

        _songId++;
        if (_songId >= _songs.Length) _songId = 0;

        PlayCurrentSong();
        _currentSongChanged?.Invoke();
    }

    private void PlayCurrentSong()
    {
        _musicInstance = AudioManager.Instance.PlaySound(gameObject, _songs[_songId]);
        _musicInstance.OnAudioFinished.AddListener(NextSong);
    }

    public void PrevSong()
    {
        StopCurrentSongForcefully();

        _songId--;
        if (_songId <= -1) _songId = _songs.Length - 1;

        PlayCurrentSong();
        _currentSongChanged?.Invoke();
    }

    private void StopCurrentSongForcefully()
    {
        if (_musicInstance == null) return;
        _musicInstance.OnAudioFinished.RemoveAllListeners();
        _musicInstance.Stop(false);
    }

    public void PlayPause()
    {
        if (!IsPaused)
            _musicInstance.Pause();
        else
            _musicInstance.Play();
    }
}
