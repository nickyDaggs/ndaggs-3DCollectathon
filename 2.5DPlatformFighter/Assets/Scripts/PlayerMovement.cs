using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    float moveSpeed;
    public Vector3 velocity = new Vector3();
    public Vector3 move = new Vector3();
    public Transform groundCheck;
    Animator animator;
    public bool animatorOff = false;
    public bool camFixed = false;
    public GameObject cam;
    float pushPower = 2.0f;
    public Animator GameView;
    public RawImage gameMenu;
    public List<Button> buttons;
    public List<Button> petButtons;
    public Animator menuTransition;
    public Animator petBottom;
    public GameObject PetsMenu;
    public List<SignDialogue> PetSigns;
    public Dialogue panel;
    public int complete = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        //petButtons[0].GetComponent<Animator>().SetTrigger("Normal");
        //StartCoroutine(Test());
        velocity.y = -2;
    }

    // Update is called once per frame
    void Update()
    {
        if (animatorOff == true)
        {
            move = new Vector3(0, 0, 0);
        }
        else
        {
            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
        moveSpeed = Mathf.Clamp(move.magnitude, 0f, 1f);
        controller.Move(move * Time.deltaTime * speed);
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        controller.Move(velocity * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, 0);
        Animate();
        /*if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameView.GetCurrentAnimatorStateInfo(0).IsName("GameIdle") && PetsMenu.activeSelf == false)
            {
                GameView.SetTrigger("Pause");
                buttons[0].Select();
                PauseGame();
            } else if (GameView.GetCurrentAnimatorStateInfo(0).IsName("GamePauseIdle") && PetsMenu.activeSelf == false)
            {
                TurnOffMenu();
            } else if(PetsMenu.activeSelf == true)
            {
                if(PetsMenu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PetSelectIdle"))
                {
                    PetToMenuTransition();
                } else if (PetsMenu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AmberMenuIdle") || PetsMenu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("RandiceMenuIdle") || PetsMenu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WaveyMenuIdle"))
                {
                    BackPetSelect();
                }

            } 
        }*/
        
    }
    void Animate()
    {
        
        if (move != Vector3.zero)
        {
            animator.SetFloat("Horizontal", move.x);
            animator.SetFloat("Vertical", move.z);
        }
        animator.SetFloat("Speed", moveSpeed);
    }
    void CamUpdate()
    {

    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;

    }
    public void PauseGame()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
        }
        StartCoroutine(Pause());
    }
    void TurnOnMenu()
    {
        foreach(Button button in buttons)
        {
            button.interactable = true;
        }
    }
    public void TurnOffMenu()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
        GameView.SetTrigger("Pause");
        PauseGame();
    }
    IEnumerator Pause()
    {
        yield return new WaitForSecondsRealtime(1f);
        if (gameMenu.enabled == false)
        {
            gameMenu.enabled = true;
            TurnOnMenu();
        }
        else if(gameMenu.enabled == true)
        {
            gameMenu.enabled = false;
        }
    }
    IEnumerator Pets()
    {
        menuTransition.SetTrigger("Change");
        
        yield return new WaitForSecondsRealtime(.5f);
        if(PetsMenu.activeSelf == false)
        {
            gameMenu.GetComponentInParent<Image>().enabled = false;
            gameMenu.enabled = false;
            PetsMenu.SetActive(true);
            if(petButtons.Count > 0)
            {
                foreach (Button button in petButtons)
                {
                    button.gameObject.SetActive(true);
                }
                petButtons = petButtons.OrderBy(go => go.name).ToList();
                petButtons[0].Select();
            }
        } else
        {
            gameMenu.GetComponentInParent<Image>().enabled = true;
            gameMenu.enabled = true;
            PetsMenu.SetActive(false);
            if (petButtons.Count > 0)
            {
                foreach (Button button in petButtons)
                {
                    button.gameObject.SetActive(false);
                }
            }
        }
        
    }
    IEnumerator Test()
    {
        yield return new WaitForSecondsRealtime(1f);
        petButtons[0].Select();
    }
    public void MenuToPetTransition()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
        StartCoroutine(Pets());
    }
    public void PetToMenuTransition()
    {
        menuTransition.SetTrigger("Change");
        
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
        buttons[0].Select();
        StartCoroutine(Pets());
    }
    public void AmberButton()
    {
        StopPetButton();
        PetsMenu.GetComponent<Animator>().SetTrigger("Amber");
        petBottom.SetTrigger("Change");
    }
    public void RandiceButton()
    {
        StopPetButton();
        PetsMenu.GetComponent<Animator>().SetTrigger("Randice");
        petBottom.SetTrigger("Change");
    }
    public void WaveyButton()
    {
        StopPetButton();
        PetsMenu.GetComponent<Animator>().SetTrigger("Wavey");
        petBottom.SetTrigger("Change");
    }
    public void BackPetSelect()
    {
        PetsMenu.GetComponent<Animator>().SetTrigger("Back");
        petBottom.SetTrigger("Change");
        foreach (Button button in petButtons)
        {
            button.interactable = true;
        }
        petButtons[0].Select();
        panel.Stop();
    }
    void StopPetButton()
    {
        foreach (Button button in petButtons)
        {
            button.interactable = false;
        }
    }
}
