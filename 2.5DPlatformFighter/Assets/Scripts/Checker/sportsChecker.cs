using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sportsChecker : MonoBehaviour
{
    public Animator winScreen;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player" && !universalChecker.Instance.doneS)
        {
            universalChecker.Instance.doneS = true;
            winScreen.SetTrigger("Win");
        }
    }
}
