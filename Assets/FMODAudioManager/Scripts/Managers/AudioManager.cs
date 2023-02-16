using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private List<AudioInstance> _audioInstances = new();

    private void Awake()
    {
        if (!Instance) Instance = this;
        else if (Instance != this) Destroy(this);
    }

    private void Update()
    {
        RemoveAllStoppedAudio();
        RepositionAll3DAudio();
    }

    private void RepositionAll3DAudio()
    {
        foreach(AudioInstance audio in _audioInstances) audio.Update3D();
    }

    private void RemoveAllStoppedAudio()
    {
        _audioInstances.RemoveAll(a => 
        {
            if(a.GetPlayback() == PLAYBACK_STATE.STOPPED)
            {
                a.OnAudioFinished?.Invoke();
                return true;
            }
            return false;
        });
    }

    public AudioInstance PlaySound(GameObject origin, AudioReference reference, float volume = 1, float pitch = 1)
    {
        if (reference.AudioEventRef.IsNull) return null;
        AudioInstance audio = new(origin, reference)
        {
            Volume = volume,
            Pitch = pitch
        };

        _audioInstances.Add(audio);

        audio.Play();
        return audio;
    }

    private void OnDisable()
    {
        _audioInstances.RemoveAll(a =>
        {
            a.Stop(true);
            return true;
        });
    }

    [Serializable]
    public class AudioInstance
    {
        // Instance settings
        private GameObject _origin;
        private AudioReference _reference;
        private EventInstance _instance;

        // Runtime settings
        public float Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                _volume = value;
                if (_instance.isValid()) _instance.setVolume(_volume);
            }
        }
        private float _volume = 1;
        public float Pitch
        {
            get
            {
                return _pitch;
            }
            set
            {
                _pitch = value;
                if (_instance.isValid()) _instance.setPitch(_pitch);
            }
        }
        private float _pitch = 1;

        // Getters
        public PLAYBACK_STATE GetPlayback()
        {
            if (!_instance.isValid()) return PLAYBACK_STATE.STOPPED;
            _instance.getPlaybackState(out PLAYBACK_STATE state);
            return state;
        }

        public bool IsPaused()
        {
            if (!_instance.isValid()) return false;
            _instance.getPaused(out bool paused);
            return paused;
        }

        // Constructors
        public AudioInstance(GameObject origin, AudioReference reference)
        {
            if (reference.AudioEventRef.IsNull) return;
            _origin = origin;
            _reference = reference;
            _instance = RuntimeManager.CreateInstance(reference.AudioEventRef);

            Update3D();
        }

        // Events
        public UnityEvent OnAudioFinished = new UnityEvent();

        // Methods
        public void Pause()
        {
            if (!_instance.isValid()) return;
            _instance.setPaused(true);
        }

        public void Play()
        {
            if (!_instance.isValid()) return;
            
            if (GetPlayback() == PLAYBACK_STATE.PLAYING && IsPaused()) _instance.setPaused(false);
            else _instance.start();
        }

        public void Stop(bool immediate)
        {
            if (!_instance.isValid()) return;
            _instance.stop(immediate ? FMOD.Studio.STOP_MODE.IMMEDIATE : FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        public void Seek(int position)
        {
            if (!_instance.isValid()) return;
            _instance.setTimelinePosition(position);
        }

        public void Update3D()
        {
            if (!_reference.Is3DAudio) return;
            if (!_instance.isValid()) return;
            _instance.set3DAttributes(_origin.To3DAttributes());
        }

        public int GetTimelinePosition()
        {
            if (!_instance.isValid()) return 0;
            _instance.getTimelinePosition(out int pos);
            return pos;
        }

        public int GetTimelineLength()
        {
            if (!_instance.isValid()) return 0;
            _instance.getDescription(out EventDescription data);
            data.getLength(out int length);
            return length;
        }
    }
}
