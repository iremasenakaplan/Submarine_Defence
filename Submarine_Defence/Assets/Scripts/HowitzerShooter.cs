using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HowitzerShooter : MonoBehaviour
{


    public Image fireButton;
    public GameObject bullet;
    public Transform[] muzzlePoses;
    int currentMuzzle = 0;
    //public GameObject muzzleEffect;

    //public ParticleSystem casing;


    GameObject[] missiles;
    



    float fireTimerBullet;

   // Transform targetTransform;
   // public Transform cross;

    public Animator turret;
    public float fireRate = 1f;
    public float firePower = 2.0f;
        public TMP_Text howitzerCountText;
            public int howitzerCount = 20;

    //bool crossAimed = false;

    Camera cam;
    //bool croshairMode = true;


    //Image crossImage;

    private List<GameObject> bulletList;
    int bulletIndex = 0;

    void Start()
    {
howitzerCountText.text = howitzerCount.ToString();
        bulletList = new List<GameObject>();

        for (int i = 0; i < 20; i++)
        {
            GameObject go = Instantiate(bullet);
            go.SetActive(false);
            go.transform.localPosition = Vector3.zero;
            bulletList.Add(go);
        }


        cam = Camera.main;
        //targetTransform = targetPoint.transform;

        // if (casing)
        //     casing.Stop();



        //crossImage = cross.GetComponent<Image>();

    }

    void FixedUpdate()
    {

        RaycastHit hit;
        //Vector3 direction = targetTransform.position - muzzlePoses[currentMuzzle].position;


       
      
        if (fireButton.fillAmount < 1)
        {
            fireButton.fillAmount += Time.deltaTime;
        }else if (fireButton.fillAmount == 1 )
        {
          
            fireButton.GetComponent<Button>().interactable = true;
            //muzzleEffect.SetActive(false);
        }
        


        




        // if (croshairMode && cross != null)
        // {
        //     Vector3 screenPos = cam.WorldToScreenPoint(targetTransform.position);
        //     if (cross.position != screenPos)
        //     {
        //         cross.position = screenPos;
        //     }
        //     Debug.Log("target is " + screenPos.x + " pixels from the left");
        // }



        // if (Physics.Raycast(muzzlePoses[currentMuzzle].position, muzzlePoses[currentMuzzle].forward*100, out hit, 4000))
        // {
        //     string t = hit.transform.tag;
        //     if (t == "enemy_helicopter" || t == "enemy_jet" || t == "enemy_balloon")
        //     {
        //         if (!crossAimed)
        //         {
        //             cross.GetComponent<Image>().color = new Color(0, 0.7f, 0.1f, 1f);
        //             crossAimed = true;
        //         }
        //     }
        //     else
        //     {
        //         if (crossAimed)
        //         {
        //             cross.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        //             crossAimed = false;
        //         }
        //     }
        // }
        // else
        // {
        //     if (crossAimed)
        //     {
        //         cross.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        //         crossAimed = false;
        //     }
        // }


    }



    public void HowitzerShoot()
    {
        if(howitzerCount>0){
            howitzerCount--;
            howitzerCountText.text = howitzerCount.ToString();
            fireButton.GetComponent<Button>().interactable = false;
            //muzzleEffect.SetActive(true);
            if (turret)
                turret.enabled = true;

            fireTimerBullet = Time.time;
            currentMuzzle++;
            if (currentMuzzle == muzzlePoses.Length)
                currentMuzzle = 0;


            if (bulletIndex >= bulletList.Count)
                bulletIndex = 0;

            bulletList[bulletIndex].SetActive(false);
            if (bulletList[bulletIndex].GetComponentInChildren<TrailRenderer>())
                bulletList[bulletIndex].GetComponentInChildren<TrailRenderer>().Clear();
            bulletList[bulletIndex].GetComponent<Rigidbody>().velocity = Vector3.zero;
            bulletList[bulletIndex].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            bulletList[bulletIndex].transform.parent = muzzlePoses[currentMuzzle].transform;
            bulletList[bulletIndex].transform.localPosition = Vector3.zero;
            bulletList[bulletIndex].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            bulletList[bulletIndex].SetActive(true);


            fireButton.fillAmount = 0;

            bulletList[bulletIndex].transform.parent = null;
            bulletList[bulletIndex].GetComponent<Rigidbody>().AddForce((muzzlePoses[0].transform.forward*100 + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) - transform.position) * firePower);

            bulletIndex++;
        }
        // if (casing)
        //     casing.Play();

    }


}

