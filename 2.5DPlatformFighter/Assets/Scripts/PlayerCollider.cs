using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class PlayerCollider : MonoBehaviour
{
    SignDialogue sign = null;
    levelPortal port = null;
    public bool camMove;
    public CinemachineBrain cam;
    public GameObject camTarget;
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public CinemachineVirtualCamera currentCam;
    Vector3 camDest;
    public float camSpeed;
    public float dist;
    public Animator GateL;
    public Animator GateR;
    Animator Switch;
    public GameObject amber;
    bool temp;
    public Animator caught;
    public float pieceCounter = 00000;
    public GameObject counter;
    public Dialogue panel;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //float step = camSpeed * Time.deltaTime;
        if (sign != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                panel.sign = sign;
                panel.on = true;
                panel.panel.enabled = true;
                sign = null;
            }
        }

        if(port != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.parent.GetComponent<CharacterController>().enabled = false;
                if (port.loadScene)
                {
                    SceneManager.LoadScene(port.SceneToLoad);
                } else
                {
                    transform.parent.position = port.teleport.position;
                }
                port = null;
                transform.parent.GetComponent<CharacterController>().enabled = true;
            }
        }
        /*
        if (Switch != null)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                
                if (Switch.GetComponent<Animator>().GetBool("Down") == false)
                {
                    Switch.GetComponent<Animator>().SetBool("Down", true);
                    if(Switch.gameObject.tag == "SwitchL")
                    {
                        GateL.SetBool("Open", true);
                    } else
                    {
                        GateR.SetBool("Open", true);
                    }
                }
                else
                {
                    Switch.GetComponent<Animator>().SetBool("Down", false);
                    if (Switch.gameObject.tag == "SwitchL")
                    {
                        GateL.SetBool("Open", false);
                    }
                    else
                    {
                        GateR.SetBool("Open", false);
                    }
                }
                Switch = null;
            }
         }   
        
        if (camMove)
        {
            dist = Vector3.Distance(camTarget.transform.position, camDest);
            float camDist = Vector3.Distance(camDest, transform.position);
            if (dist > .01f)
            {
                camTarget.transform.localPosition = Vector3.MoveTowards(camTarget.transform.position, camDest, step);
            } else
            {
                if (camDest == Vector3.zero)
                {

                }
                currentCam.GetComponent<CinemachineVirtualCamera>().Follow = null;
                camMove = false;
            }
        } else
        {
            
        }
        currentCam.gameObject.SetActive(true);
        if (temp == true)
        {
            //GateL.SetBool("Open", true);
            //GateR.SetBool("Open", true);
            //Debug.Log("P");
        }*/
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sign")
        {
            sign = other.GetComponent<SignDialogue>();
        } else if(other.gameObject.tag == "Hitbox")
        {
            port = other.GetComponent<levelPortal>();
        }
        
        /*else if (other.gameObject.tag == "camCollider")
        {
            camTarget.transform.localPosition = other.GetComponent<camColliderScript>().camTarget;
            if (currentCam == cam1)
            {
                currentCam.gameObject.SetActive(false);
                currentCam = cam2;
                StartCoroutine(camChange());
            } else if (currentCam == cam2)
            {
                currentCam.gameObject.SetActive(false);
                currentCam = cam1;
            }
        } else if(other.gameObject.tag == "SwitchL" || other.gameObject.tag == "SwitchR")
        {
            Switch = other.GetComponent<Animator>();
        } else if(other.gameObject.tag == "PetA")
        {
            amber.GetComponent<Animator>().SetBool("Jump", false);
            amber.GetComponent<Animator>().SetBool("dead", true);
            temp = true;
            Debug.Log("Pet Collected");
        } else if (other.gameObject.tag == "Bucket")
        {
            other.GetComponent<Animator>().SetTrigger("Caught");
        } else if(other.gameObject.tag == "piece")
        {
            other.GetComponent<pieceScript>().collected = true;
            counter.GetComponent<counterScript>().pieceCounter += 1;
            
            TurnOnCounter();
        }*/
    }
    void TurnOnCounter()
    {
        if (counter.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CounterOffIdle") || counter.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CounterOff"))
        {
            counter.GetComponent<Animator>().SetTrigger("On");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        sign = null;
        port = null;
    }

    IEnumerator camChange()
    {
        yield return new WaitForSeconds(.1f);
        currentCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = .5f;
    }

}
