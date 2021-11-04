using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetArea : MonoBehaviour
{
    public Vector3 ballRespawn;
    public Transform player;
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = ballRespawn;
        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
