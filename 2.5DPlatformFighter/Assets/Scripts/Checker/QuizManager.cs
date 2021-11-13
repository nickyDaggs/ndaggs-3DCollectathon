using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class QuizManager : MonoBehaviour
{
    public Animator win;

    public Text qText;
    public List<string> questions;
    public List<GameObject> questionParts;

    public List<PuzzleSlot> slots;

    int curQ = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!universalChecker.Instance.doneL && slots.All(a => a.solved))
        {
            Correct();
            universalChecker.Instance.doneL = true;
            win.SetTrigger("Win");
        }
    }

    public void Correct()
    {
        questionParts[curQ].SetActive(false);
        curQ++;
        qText.text = questions[curQ];
        questionParts[curQ].SetActive(true);
    }

    public void End()
    {
        SceneManager.LoadScene(1);
    }
}
