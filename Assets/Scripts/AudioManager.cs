using UnityEngine;
using System;
using UnityEngine.Audio;

// ideja no brackeys šo es saglabāšu, tik vieglu padara visu audio vadību tikai ar vienu.
// Iepriekš stundu nočakarējos, bija briesmīgi, visas skaņas bija, bet strādāja nepareizi, nepareizā laikā
// ar šito es varēju 25 minūšu laikā sataisīt visu, ka ir labāk, pat nekā es biju gaidījis

public class AudioManager : MonoBehaviour
{

    public AudioSound[] sounds;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        foreach (AudioSound s in sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.priority = s.priority;
            s.source.spatialBlend = s.spatialBlend;
        }
    }

    public void Play (string name)
    {
        AudioSound s = Array.Find(sounds, sound => sound.name == name);
        s.source.PlayOneShot(s.source.clip, s.volume);
    }

    public void Stop(string name)
    {
        AudioSound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

}
