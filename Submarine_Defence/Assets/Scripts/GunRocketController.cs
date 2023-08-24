using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunRocketController : MonoBehaviour
{
    public GameObject roketPrefab;
    public Transform firlatNoktasi;
    public float firlatGucu = 10f;
    AudioSource audioSource;
    public Slider healthSliderB;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void FirlatRoket()
    {
        GameObject yeniRoket = Instantiate(roketPrefab, firlatNoktasi.position, firlatNoktasi.rotation);

        Rigidbody roketRigidbody = yeniRoket.GetComponent<Rigidbody>();
        roketRigidbody.AddForce(firlatNoktasi.forward * firlatGucu, ForceMode.Impulse);
        audioSource.Play();
    }

    public void DecreaseHealth(float amount)
    {
        healthSliderB.value -= amount;
    }
}
