using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class universalChecker : MonoBehaviour
{
    private static universalChecker _instance;

    public static universalChecker Instance { get { return _instance; } }

    public bool doneM, doneF, doneS, doneR, doneL, doneH;

    // Start is called before the first frame update
    void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(doneF && doneH && doneL && doneM && doneR && doneS)
        {
            SceneManager.LoadScene(4);
            doneS = false;
        }
    }

    public void End()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
}
