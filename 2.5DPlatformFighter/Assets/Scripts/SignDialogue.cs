using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action { EnableObj, Cutscene, Damage, Sound, Activity };


public class SignDialogue : MonoBehaviour
{
    

    public GameObject player;
    public List<string> Lines = new List<string>();
    public bool action;

    public Action actionToDo;
    public List<GameObject> objs = new List<GameObject>();

    //public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Interact()
    {
        
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetTrigger("Change");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetTrigger("Change");
        }
    }*/
}
