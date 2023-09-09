using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MissileDestroy : MonoBehaviour
{
     public GameObject blastMissile;
    // public GameObject blastWater;
    // public GameObject blastMachine;
    //public float effectDestroyDelay = 1.0f;
    public float damage = 1.0f;
    // public GameObject blastBig;
    // public GameObject fire;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Border"){
            BulletImpactPool.Instance.GetBulletHitGround().transform.position = transform.position;
            //Destroy(Instantiate(blastGround, transform.position , blastGround.transform.rotation), effectDestroyDelay);
            //Destroy (this.gameObject);
            this.gameObject.SetActive(false);
        }else if(other.gameObject.tag == "Water"){
            BulletImpactPool.Instance.GetBulletHitWater().transform.position = transform.position;
            //Destroy(Instantiate(blastWater, transform.position , blastWater.transform.rotation), effectDestroyDelay);
            //Destroy (this.gameObject);
            this.gameObject.SetActive(false);
        }else if(other.gameObject.tag == "Machine"){
            BulletImpactPool.Instance.GetBulletHitMetal().transform.position = transform.position;
            //Destroy(Instantiate(blastMachine, transform.position , blastMachine.transform.rotation), effectDestroyDelay);
            //Destroy (this.gameObject);
            this.gameObject.SetActive(false);
        }else if(other.gameObject.tag == "HitSmoke"){
            //Destroy (this.gameObject);
            this.gameObject.SetActive(false);
        }else if(other.gameObject.tag == "Flare"){
            //Destroy (this.gameObject);
            this.gameObject.SetActive(false);
        }else if(other.gameObject.tag == "Far"){
            //Destroy (this.gameObject);
            this.gameObject.SetActive(false);
        }
        // if(this.transform.tag != "missile")
             this.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Border"){
            BulletImpactPool.Instance.GetBulletHitGround().transform.position = transform.position;
            //Destroy(Instantiate(blastGround, collision.contacts[0].point , blastGround.transform.rotation), effectDestroyDelay);
            //Destroy (this.gameObject);
            this.gameObject.SetActive(false);
        }else if(collision.gameObject.tag == "Water"){
            BulletImpactPool.Instance.GetBulletHitWater().transform.position = transform.position;
            //Destroy(Instantiate(blastWater, collision.contacts[0].point , blastWater.transform.rotation), effectDestroyDelay);
            //Destroy (this.gameObject);
            this.gameObject.SetActive(false);
        }else if(collision.gameObject.tag == "Machine"){
            BulletImpactPool.Instance.GetBulletHitMetal().transform.position = transform.position;
            //Destroy(Instantiate(blastMachine, transform.position , blastMachine.transform.rotation), effectDestroyDelay);
            //Destroy (this.gameObject);
            this.gameObject.SetActive(false);
        }else if(collision.gameObject.tag == "HitSmoke"){
            //Destroy (this.gameObject);
            this.gameObject.SetActive(false);
        }else if(collision.gameObject.tag == "Flare"){
            //Destroy (this.gameObject);
            this.gameObject.SetActive(false);
        }else if(collision.gameObject.tag == "Far"){
            //Destroy (this.gameObject);
            this.gameObject.SetActive(false);
        }
      
        this.gameObject.SetActive(false);
    }

    public void DestroyOnIntercept(){
        BulletImpactPool.Instance.GetBulletHitGround().transform.position = transform.position;
       // Destroy(Instantiate(blastGround, transform.position , blastGround.transform.rotation), effectDestroyDelay);
       // Destroy (this.gameObject);
        this.gameObject.SetActive(false);
    }

    public void DestroyOnHitMissile(){
        //BulletImpactPool.Instance.GetBulletHitGround().transform.position = transform.position;
        //Destroy(Instantiate(blastGround, transform.position , blastGround.transform.rotation), effectDestroyDelay);
        Destroy(Instantiate(blastMissile, transform.position , blastMissile.transform.rotation), 2f);
        Destroy (this.gameObject);
        //this.gameObject.SetActive(false);
    }

    // private void OnBecameInvisible() {
    //     this.gameObject.SetActive(false);
    // }
}
