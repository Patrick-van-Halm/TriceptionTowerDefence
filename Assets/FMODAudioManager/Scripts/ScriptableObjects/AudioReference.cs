using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Reference", menuName = "Scriptable Objects/Audio/Reference")]
public class AudioReference : ScriptableObject
{
    public EventReference AudioEventRef;
    public bool Is3DAudio;
}