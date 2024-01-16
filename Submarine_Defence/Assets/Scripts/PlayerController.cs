using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick; // Joystick referans�
    public float rotationSpeed = 10f; // Oyuncunun d�nme h�z�
    public float health = 100f; 

    void Update()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        Camera.main.transform.Rotate(Vector3.right, -verticalInput * rotationSpeed * Time.deltaTime);
    }

    public void SetHealth(float hp){
        health = hp;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyBullet"){
            health-=collision.gameObject.GetComponent<MissileDestroy>().damage;
            collision.gameObject.SetActive(false);
            UIManager.Instance.GetDamage();
            UIManager.Instance.SetHealthBar(health);
            if(health<=0){
                UIManager.Instance.LoseGame();
                GoogleMobileAdsDemoScript.Instance.ShowIntertitialAd();
            }
        }
    }

}
