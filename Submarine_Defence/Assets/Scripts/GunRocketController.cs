using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunRocketController : MonoBehaviour
{
    public GameObject roketPrefab;
    public Transform firlatNoktasi;
    public float firlatGucu = 10f;
    public Button fireButton;
    public Image fireButtonImage;
    public Image cross;
    public TMP_Text torpedoCountText;
    public int torpedoCount = 10;

    // AudioSource audioSource;
    //public Slider healthSliderB;

    private void Start()
    {
        torpedoCountText.text = torpedoCount.ToString();
    }

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
        if(torpedoCount>0){
            GameObject yeniRoket = Instantiate(roketPrefab, firlatNoktasi.position, firlatNoktasi.rotation);

            Rigidbody roketRigidbody = yeniRoket.GetComponent<Rigidbody>();
            roketRigidbody.AddForce(firlatNoktasi.forward * firlatGucu, ForceMode.Impulse);
            fireButton.interactable = false;
            fireButtonImage.fillAmount = 0;
            Destroy(yeniRoket, 20);
            if(torpedoCount>0)
            torpedoCount-=1;
            torpedoCountText.text = torpedoCount.ToString();
            if(torpedoCount==0){
                StartCoroutine(LoseByMissileFinished());
            }
        }
     //   audioSource.Play();
    }

    IEnumerator LoseByMissileFinished(){
        yield return new WaitForSeconds(20);
        UIManager.Instance.LoseGame();
    }

    //public void DecreaseHealth(float amount)
    //{
     //   healthSliderB.value -= amount;
    //}
}
