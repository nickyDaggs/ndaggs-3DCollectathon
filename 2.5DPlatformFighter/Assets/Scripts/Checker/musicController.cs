using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicController : MonoBehaviour
{
    public List<AudioClip> parts;
    public List<SignDialogue> instruments;
    public AudioSource Audio;
    public Animator WRONG;
    public Animator winScreen;
    int part;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPart(SignDialogue dia)
    {
        if(instruments.IndexOf(dia) == part)
        {
            Audio.clip = parts[part];
            Audio.Play();
            part++;
            if(part >= instruments.Count)
            {
                universalChecker.Instance.doneM = true;
                winScreen.SetTrigger("Win");
            }
        } else
        {
            if(part < instruments.Count && !universalChecker.Instance.doneM)
            {
                Audio.clip = null;
                WRONG.SetTrigger("WRONG");
                part = 0;
                
            }
        }
    }
}
