using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Missile : MonoBehaviour
{

    public GameObject explosionEffect;
    AudioSource _audioSource;
     public void Start()
     {
         _audioSource = GetComponent<AudioSource>();
   }

    private void OnCollisionEnter(Collision collision)
     {

     if (collision.gameObject.CompareTag("battleship"))
     {
       Instantiate(explosionEffect, transform.position, transform.rotation);
       Destroy(gameObject);

        //    GunRocketController battleshipHealth = collision.gameObject.GetComponent<GunRocketController>();
          //  if (battleshipHealth != null)
         //   {
          //      battleshipHealth.DecreaseHealth(1);
          //  }

        }
   }

   

}
