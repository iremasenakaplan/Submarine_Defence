using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunRocketController : MonoBehaviour
{
    public GameObject roketPrefab;
    public Transform firlatNoktasi;
    public float firlatGucu = 10f;
    public Button fireButton;
    public Image fireButtonImage;
    public Image cross;

    // AudioSource audioSource;
    //public Slider healthSliderB;

    //private void Start()
    // {
    //  audioSource = GetComponent<AudioSource>();
    //  }

    private void Update()
    {
        if (fireButtonImage.fillAmount < 1) {
            fireButtonImage.fillAmount += Time.deltaTime/2.0f;
            if (fireButtonImage.fillAmount >= 1) {
                fireButton.interactable = true;
            }
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "battleship")
            {
                cross.color = Color.red;
            }
            else
            {
                cross.color = Color.green;
            }
            // now do second raycast from hit.point to gun.nozzle
        }
        else {
            cross.color = Color.green;
        }

       
    }

    public void FirlatRoket()
    {
        GameObject yeniRoket = Instantiate(roketPrefab, firlatNoktasi.position, firlatNoktasi.rotation);

        Rigidbody roketRigidbody = yeniRoket.GetComponent<Rigidbody>();
        roketRigidbody.AddForce(firlatNoktasi.forward * firlatGucu, ForceMode.Impulse);
        fireButton.interactable = false;
        fireButtonImage.fillAmount = 0;
        Destroy(yeniRoket, 15);
     //   audioSource.Play();
    }

    //public void DecreaseHealth(float amount)
    //{
     //   healthSliderB.value -= amount;
    //}
}
