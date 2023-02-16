using FMOD.Studio;
using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Channel", menuName = "Scriptable Objects/Audio/Channel")]
public class AudioChannel : ScriptableObject
{
    // Audio channels or VCA's can be setup in FMOD's mixer where you can also assign the VCA to an event
    [Tooltip("The name of the channel (FMod: VCA) without the prefix vca:/")]
    [SerializeField] private string _channelName;
    public float Volume
    {
        get
        {
            RuntimeManager.GetVCA("vca:/" + _channelName).getVolume(out float volume);
            return volume;
        }
        set
        {
            RuntimeManager.GetVCA("vca:/" + _channelName).setVolume(value);
        }
    }
}