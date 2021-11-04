using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip clip;

    public void Play()
    {
        Audio.PlayOneShot(clip);
    }
}
