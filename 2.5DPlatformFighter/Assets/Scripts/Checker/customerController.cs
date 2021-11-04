using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customerController : MonoBehaviour
{
    public List<Transform> customers;
    public List<Transform> foods;
    public Animator win;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       for(int i = 0; i < customers.Count; i++)
        {
            if(Vector3.Distance(customers[i].position, foods[i].position) < 1)
            {
                foods[i].GetComponent<Rigidbody>().isKinematic = true;
                foods.Remove(foods[i]);
                customers.Remove(customers[i]);
            }
        }
       if(customers.Count < 1 && !universalChecker.Instance.doneF)
       {
            universalChecker.Instance.doneF = true;
            win.SetTrigger("Win");
       }
    }
}
