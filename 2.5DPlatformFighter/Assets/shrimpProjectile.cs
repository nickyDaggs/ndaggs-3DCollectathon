using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrimpProjectile : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Destroy(transform.parent.gameObject, 4.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hitbox")
        {
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Animator>().SetTrigger("attack");
        }
    }
}
