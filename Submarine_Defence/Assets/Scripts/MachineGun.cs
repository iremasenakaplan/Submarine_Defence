using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.Audio;


public class MachineGun : MonoBehaviour
{

    public GameObject bullet;

    public Transform[] muzzlePoses;
    int currentMuzzle = 0;
 
  
    // public ParticleSystem muzzleSmoke;
    // public ParticleSystem casing;

    GameObject[] missiles;

 
    public bool isSoundPlayer = false;
    bool isPressed = false;
   
    float fireTimerBullet;
    
   
 

    public float fireRate = 0.003f;
    public float firePower = 2.0f;
    

    AudioSource musicSource;
    public AudioMixer SFX_Mixer;
    public AudioMixerGroup StartAndLoopMixerGroup;
    

    bool fadeout = false;
    double startTime ;
    public AudioSource audioFX_start, audioFX_loop, audioFX_end;

   


    private List<GameObject> bulletList; 
    int bulletIndex = 0;

    void Start()
    {

        bulletList = new List<GameObject>();

        for(int i=0; i<400; i++){
            GameObject go = Instantiate(bullet);
            go.SetActive(false);
            go.transform.localPosition = Vector3.zero;
            bulletList.Add(go);
        }


 
        // if(muzzleSmoke)
        //     muzzleSmoke.Stop(true);
        // if(casing)
        //     casing.Stop();

        if(isSoundPlayer){
            musicSource = this.GetComponent<AudioSource>();
            musicSource.Play();
            startTime = AudioSettings.dspTime;
        }

    }

    void FixedUpdate(){
    
        

        RaycastHit hit;

        
        
        if(isPressed){

      

            if (Time.time >= fireRate + fireTimerBullet)
            {   
                
                fireTimerBullet = Time.time;
                currentMuzzle++;
                if(currentMuzzle==muzzlePoses.Length)
                    currentMuzzle = 0;
       
                if(bulletIndex>=bulletList.Count)
                        bulletIndex = 0;
 
                bulletList[bulletIndex].SetActive(false);
                // if(bulletList[bulletIndex].GetComponentInChildren<TrailRenderer>())
                //     bulletList[bulletIndex].GetComponentInChildren<TrailRenderer>().Clear();
                bulletList[bulletIndex].GetComponent<Rigidbody>().velocity = Vector3.zero;
                bulletList[bulletIndex].GetComponent<Rigidbody>().angularVelocity = Vector3.zero; 
                bulletList[bulletIndex].transform.parent = muzzlePoses[currentMuzzle].transform;//Instantiate (Bullet, Muzzles[currentMuzleIndex].transform.position, Muzzles[currentMuzleIndex].transform.rotation);
                bulletList[bulletIndex].transform.localPosition = Vector3.zero;
                bulletList[bulletIndex].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                bulletList[bulletIndex].SetActive(true);


                bulletList[bulletIndex].transform.parent = null;
                bulletList[bulletIndex].GetComponent<Rigidbody>().AddForce((muzzlePoses[currentMuzzle].forward + new Vector3(Random.Range(-0.02f, 0.02f),Random.Range(-0.02f, 0.02f),Random.Range(-0.02f, 0.02f))) * firePower);
                bulletIndex++;
                
          
            } 
        }

    }


    public void OnPointerDown()
    {
       
            isPressed = true;
            
    

          
    
        
            // if(muzzleSmoke)
            //     muzzleSmoke.Play(true);
            // if(casing)
            //     casing.Play();

            if(isSoundPlayer){
                fadeout = false;
                StartAndLoopMixerGroup.audioMixer.SetFloat("StartLoopGroup_Volume", 0f);
                float randomPitch = Random.Range(0.97f, 1.03f);
                SFX_Mixer.SetFloat("SFX_Pitch", randomPitch);
                audioFX_start.PlayScheduled(startTime);
                audioFX_loop.PlayScheduled(startTime + audioFX_start.clip.length / randomPitch);
            }
        
        
    }
    public void OnPointerUp()
    {
        isPressed = false;

      
        

        // if(casing)
        //     casing.Stop();

        if(isSoundPlayer){
            fadeout = true;
            audioFX_loop.Stop();
            audioFX_end.Play();
        }
    }
}
