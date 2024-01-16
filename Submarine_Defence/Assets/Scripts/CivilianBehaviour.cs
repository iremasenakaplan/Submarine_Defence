using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathCreation.Examples;
using DG.Tweening;
using QuantumTek.QuantumTravel;

public class CivilianBehaviour : MonoBehaviour
{

   
    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject firingEffect;
    [SerializeField] GameObject[] parts;
    [SerializeField] GameObject body;
    [SerializeField] Material destroyedMaterial;
    bool isFiring = false;
    bool died = false;
    public GameManager gameManager;


    private void OnCollisionEnter(Collision collision)
    {
        DOTween.Init();
        if (collision.gameObject.CompareTag("missile") && !died)
        {
            
            
               
          
                died = true;
                transform.DOMove(new Vector3(transform.position.x, transform.position.y-14, transform.position.z), 6);
                transform.DORotate(new Vector3(Random.Range(0, 40),Random.Range(0, 90),Random.Range(0, 90)), 8);
                Instantiate(explosionEffect, transform.position, transform.rotation);
                body.GetComponent<MeshRenderer>().material = destroyedMaterial;
                foreach (GameObject part in parts) {
                    Rigidbody r = part.AddComponent<Rigidbody>();
                    r.transform.parent = null;
                    r.AddExplosionForce(200, collision.transform.position, 15, 15.0F);
                    
                   // Destroy(part, 3);
                }
                GetComponent<Rigidbody>().isKinematic = false;
                //Destroy(this.gameObject);
                Destroy(this.gameObject, 5f);
                GetComponent<PathFollower>().enabled = false;
              
                UIManager.Instance.LoseGame();
                GoogleMobileAdsDemoScript.Instance.ShowIntertitialAd();


            Destroy(collision.gameObject);

        }
    }
}
