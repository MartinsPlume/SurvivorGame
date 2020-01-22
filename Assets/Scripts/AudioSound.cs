using UnityEngine;
using UnityEngine.Audio;

// Brackeys sound klase --> fantastiska ideja

[System.Serializable]
public class AudioSound
{
    public string name;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    public bool loop;

    [Range(0, 256)]
    public int priority;
    [Range(0f, 1f)]
    public float spatialBlend;


    [HideInInspector]
    public AudioSource source;
}
