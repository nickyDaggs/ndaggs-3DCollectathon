using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    List<string> Lines = new List<string>();
    int current = 0;
    public TextMeshProUGUI textBox1;
    public TextMeshProUGUI textBox2;
    public Image panel;
    public bool on;
    public SignDialogue sign;
    float textDelay = 0.1f;
    IEnumerator coroutine;
    public static Dialogue instance;
    bool skip = false;
    public PlayerMovement player;
    public bool pets;
    public musicController music;
    public Animator winScreen;
    public Animator boss;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    private void Update()
    {
        if (on == true)
        {
            panel.enabled = true;
            textBox1.enabled = true;
            textBox1.text = "";
            
            textBox2.enabled = false;
            Lines = sign.Lines;
            StartCoroutine("Text");
            player.controller.enabled = false;
            player.animatorOff = true;
            on = false;
        }
        else
        {
            
        }
        if (textBox1 != null && Lines.Count > 0)
        {
            if (textBox1.text == Lines[current] || textBox2.text == Lines[current])
            {
                if (Input.GetKeyDown(KeyCode.Space) && current < Lines.Count - 1)
                {
                    current++;
                    textBox1.text = "";
                    textBox2.text = "";
                    on = true;
                    skip = false;

                }
                else if (Input.GetKeyDown(KeyCode.Space) && current >= Lines.Count - 1)
                {
                    Stop();
                    if(pets == true)
                    {
                        player.BackPetSelect();
                    }
                }
            }
            else if(textBox1.text != Lines[current] && textBox1.text.Length > 3)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StopCoroutine("Text");
                    textBox1.enabled = false;
                    textBox2.enabled = true;
                    textBox2.text = "";
                    skip = true;
                    textBox2.text = Lines[current];
                }
            }
            if (skip == true)
            {
                textBox1.text = "";
            } else
            {

            }
        }
    }

    // Update is called once per frame
        public IEnumerator Text()
        {
            foreach (char letter in Lines[current])
            {
                textBox1.text += letter;
                yield return new WaitForSecondsRealtime(textDelay);
            }
            textDelay = 0.1f;
        }
    public void Stop()
    {
        if (sign.action)
        {
            DoAction();
        }
        panel.enabled = false;
        current = 0;
        textBox1.text = "";
        textBox1.enabled = false;
        textBox2.text = "";
        textBox2.enabled = false;
        sign = null;
        player.controller.enabled = true;
        player.animatorOff = false;
        Lines = new List<string>();
        skip = false;

    }
    public void DoAction()
    {
        switch(sign.actionToDo)
        {
            case Action.EnableObj:
                foreach(GameObject obj in sign.objs)
                {
                    obj.SetActive(true);
                }
                break;
            case Action.Damage:
                boss.SetTrigger("Damage");
                break;

            case Action.Cutscene:

                break;

            case Action.Sound:
                if(!universalChecker.Instance.doneM)
                {
                    music.NextPart(sign);
                }
                break;
            case Action.Activity:
                if(!universalChecker.Instance.doneR)
                {
                    universalChecker.Instance.doneR = true;
                    winScreen.SetTrigger("Win");
                }
                break;
        }
    }
}
    
