using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0.1f, 3f)]
    public float pitch;
    public bool loop;
    public bool randomPitch;
    [HideInInspector]
    public AudioSource source;
}
