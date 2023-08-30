using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] Slider enemyHealthbar;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject firingEffect;
    [SerializeField] GameObject[] parts;
    [SerializeField] Material destroyedMaterial;
    bool isFiring = false;
    public GameManager gameManager;


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("missile"))
        {
            enemyHealthbar.value -= 35;
            if (enemyHealthbar.value <= 0) {
                Instantiate(explosionEffect, transform.position, transform.rotation);
                GetComponent<MeshRenderer>().material = destroyedMaterial;
                foreach (GameObject part in parts) {
                    Rigidbody r = part.AddComponent<Rigidbody>();
                    r.AddExplosionForce(600, collision.transform.position, 15, 15.0F);
                    
                   // Destroy(part, 3);
                }
                GetComponent<Rigidbody>().isKinematic = false;
                //Destroy(this.gameObject);
                GameManager.Instance.EnemyKilled();
            }

            if (enemyHealthbar.value <= 50 && enemyHealthbar.value > 0 && !isFiring) {
                Instantiate(firingEffect, transform);
                isFiring = true;
            }

        }
    }
}
