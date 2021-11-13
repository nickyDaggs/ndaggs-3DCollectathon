using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundEffect : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip clip;
    public GameObject Button;
    public bool loop;

    public void Play()
    {
        if(loop)
        {
            Audio.clip = clip;
            Audio.Play();
        } else
        {
            Audio.PlayOneShot(clip);
        }
    }

    public void Load()
    {
        SceneManager.LoadScene(5);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ButtonS()
    {
        Button.SetActive(true);
    }
}
