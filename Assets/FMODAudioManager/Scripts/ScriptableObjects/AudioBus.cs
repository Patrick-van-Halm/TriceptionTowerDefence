using FMOD.Studio;
using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Bus", menuName = "Scriptable Objects/Audio/Bus")]
public class AudioBus : ScriptableObject
{
    [Tooltip("Leave empty to target the master bus")]
    [SerializeField] private string _busName;

    public float Volume
    {
        get
        {
            RuntimeManager.GetBus("bus:/" + _busName).getVolume(out float volume);
            return volume;
        }
        set
        {
            RuntimeManager.GetBus("bus:/" + _busName).setVolume(value);
        }
    }
}