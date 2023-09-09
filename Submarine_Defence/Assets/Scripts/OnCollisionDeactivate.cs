using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionDeactivate : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        this.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {

        this.gameObject.SetActive(false);
    }
}
