using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SparseDesign;
using SparseDesign.ControlledFlight;

public class GuidedShooter : MonoBehaviour
{
 
    public GameObject guidedRoketPrefab;
    public Transform firlatNoktasi;
    public float firlatGucu = 10f;
    public Button fireButton;
    public Image fireButtonImage;

   
    public TMP_Text guidedTorpedoCountText;
   
    public int guidedTorpedoCount = 0;

    private GameObject lastTarget;

    // AudioSource audioSource;
    //public Slider healthSliderB;

    private void Start()
    {
    
        if(guidedTorpedoCountText!=null)
            guidedTorpedoCountText.text = guidedTorpedoCount.ToString();
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
                //cross.color = Color.red;
                lastTarget = hit.transform.gameObject;
            }
            else
            {
                //cross.color = Color.green;
            }
            // now do second raycast from hit.point to gun.nozzle
        }
        else {
            //cross.color = Color.green;
        }

       
    }

    

    public void FirlatGuidedRoket()
    {
        if(guidedTorpedoCount>0){
            GameObject yeniRoket = Instantiate(guidedRoketPrefab, firlatNoktasi.position, firlatNoktasi.rotation);
            GameObject emptyTarget = new GameObject();
            emptyTarget.transform.position = firlatNoktasi.forward *1000;
            if(lastTarget)
                yeniRoket.transform.GetComponent<MissileSupervisor>().m_guidanceSettings.m_target = lastTarget;
            else
                yeniRoket.transform.GetComponent<MissileSupervisor>().m_guidanceSettings.m_target = emptyTarget;
            //Rigidbody roketRigidbody = yeniRoket.GetComponent<Rigidbody>();
            //roketRigidbody.AddForce(firlatNoktasi.forward * firlatGucu, ForceMode.Impulse);
            fireButton.interactable = false;
            fireButtonImage.fillAmount = 0;
            Destroy(yeniRoket, 25);
            if(guidedTorpedoCount>0)
                guidedTorpedoCount-=1;
            guidedTorpedoCountText.text = guidedTorpedoCount.ToString();
            //if(guidedTorpedoCount==0 ){
             //   StartCoroutine(LoseByMissileFinished());
            //}
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
