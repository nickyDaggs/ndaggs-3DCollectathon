using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class holidayManager : MonoBehaviour
{
    public List<PuzzleSlot> slots;

    public Button end;

    public Animator win;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!universalChecker.Instance.doneH && slots.All(a => a.solved))
        {
            end.interactable = true;
            universalChecker.Instance.doneH = true;
            win.SetTrigger("Win");
        }
    }

    public void End()
    {
        SceneManager.LoadScene(1);
    }
}
