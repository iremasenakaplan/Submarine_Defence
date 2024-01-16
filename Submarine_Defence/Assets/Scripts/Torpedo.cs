using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Torpedo : MonoBehaviour
{

    public GameObject explosionEffect;
    public int damage;
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
