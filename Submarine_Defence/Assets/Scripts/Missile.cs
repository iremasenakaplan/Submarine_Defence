using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
   
    public GameObject explosionEffect;

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("battleship"))
        {
         
            Instantiate(explosionEffect, transform.position, transform.rotation);
           
            Destroy(gameObject);
        }
    }



}
