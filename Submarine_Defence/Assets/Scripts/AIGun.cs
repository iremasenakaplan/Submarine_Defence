using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGun : MonoBehaviour
{
    public string[] TargetTag ;

	public GameObject turret;
	public GameObject gun;
	public GameObject Out;
	public GameObject[] Muzzles;

	public Vector3 eulerAngles;
	public Vector3 eulerAngles2;
	public Vector3 startPos;
	public float AngleLimitUp = 80f;
	public float AngleLimitDn = 10f;

	public GameObject Bullet;
	public int Ammo = 100;
	public int bulletPerFire = 30;

	float timeAIattackTmp;

	float time;
	 float distance;
	public float range;
	bool Fire;

	public GameObject target;

    public float bulletWaitTime = 0.1f;
    float bulletDelay = 0f;

    int currentFiredBullet = 0;
    int currentMuzleIndex = 0;

    private List<GameObject> bulletList; 
    int bulletIndex = 0;
	void Start () {

        bulletList = new List<GameObject>();

        for(int i=0; i<bulletPerFire*2; i++){
            GameObject go = Instantiate(Bullet);
            go.SetActive(false);
            //go.transform.parent = Muzzles[i%Muzzles.Length].transform;
            go.transform.localPosition = Vector3.zero;
            bulletList.Add(go);
        }

		timeAIattackTmp = Time.time + Random.Range(5f, 10f);
        startPos = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
	}
	
	
	void Update ()
	{
		if(target != null){
			distance = Vector3.Distance(transform.position, target.transform.position);


			if(distance < range){	
                
 			    eulerAngles = transform.rotation.eulerAngles;
			    eulerAngles = new Vector3(0, eulerAngles.y, 0);
                transform.rotation = Quaternion.Euler(eulerAngles);
                Quaternion targetlook = Quaternion.LookRotation(target.transform.position+ new Vector3(Random.Range(-2,2),Random.Range(-10,-5),Random.Range(-2,2)) - turret.transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation,targetlook,Time.deltaTime * 3);

                eulerAngles2 = gun.transform.rotation.eulerAngles;
                eulerAngles2 = new Vector3(eulerAngles2.x , eulerAngles.y, 0);
                gun.transform.rotation = Quaternion.Euler(eulerAngles2);
                if(-AngleLimitUp < eulerAngles2.x || AngleLimitDn < eulerAngles2.x)
                gun.transform.rotation = Quaternion.Lerp(gun.transform.rotation,targetlook,Time.deltaTime * 3);

                
        
                time = Time.time;
                    
                if( time > timeAIattackTmp && Bullet != null && Ammo > 0 && !Fire)
                {
                    Fire = true;
                    Out.gameObject.SetActive(true);
                }

                if(Fire && currentFiredBullet<bulletPerFire && bulletDelay >= bulletWaitTime){
                    bulletDelay = 0f;
                    currentFiredBullet++;
                    currentMuzleIndex++;
                    if(currentMuzleIndex>=Muzzles.Length)
                        currentMuzleIndex = 0;
                    
                    if(bulletIndex>=bulletList.Count-1)
                        bulletIndex = 0;
                   // GameObject b = bulletList[i];
                    bulletList[bulletIndex].gameObject.SetActive(false);
                    bulletList[bulletIndex].transform.parent = Muzzles[currentMuzleIndex].transform;//Instantiate (Bullet, Muzzles[currentMuzleIndex].transform.position, Muzzles[currentMuzleIndex].transform.rotation);
                    bulletList[bulletIndex].transform.localPosition = Vector3.zero;
                    bulletList[bulletIndex].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    bulletList[bulletIndex].transform.parent = null;
                    bulletList[bulletIndex].SetActive(true);
                    if(bulletList[bulletIndex].GetComponent<TrailRenderer>())
                        bulletList[bulletIndex].GetComponent<TrailRenderer>().Clear();

                    //Rigidbody rb = bulletList[bulletIndex].GetComponent<Rigidbody>();
                    bulletList[bulletIndex].GetComponent<Rigidbody>().velocity = Vector3.zero;
                    bulletList[bulletIndex].GetComponent<Rigidbody>().angularVelocity = Vector3.zero; 
                    bulletList[bulletIndex].GetComponent<Rigidbody>().AddForce(bulletList[bulletIndex].transform.forward *20, ForceMode.Impulse); 
                    bulletIndex++;
                    //Destroy(Instantiate (Bullet, Muzzles[currentMuzleIndex].transform.position, Muzzles[currentMuzleIndex].transform.rotation), 4f); 
                    //Ammo = Ammo - 1;            
                    Fire = false;
                    
                }else if(Fire && currentFiredBullet<bulletPerFire && bulletDelay < bulletWaitTime){
                    bulletDelay+= Time.deltaTime;
                }else if(Fire && currentFiredBullet>=bulletPerFire){
                    timeAIattackTmp =  time + Random.Range(3f, 8f) ;
                    Fire = false;
                    currentFiredBullet = 0;
                    bulletDelay = 0;
                    Out.gameObject.SetActive(false);
                }

            }else{
				transform.rotation = Quaternion.Euler(eulerAngles);
				Quaternion StartPos = Quaternion.LookRotation(startPos);
				transform.rotation = Quaternion.Lerp(transform.rotation,StartPos,Time.deltaTime * 2);
			}

		}else{
            if(Out.gameObject.activeSelf)
                Out.gameObject.SetActive(false);
			for(int t = 0; t<TargetTag.Length; t++)
            {
                
                // AI find target only in TargetTag list
                GameObject[] objs = GameObject.FindGameObjectsWithTag(TargetTag[t]);
                if (objs.Length > 0)
                {
                    Debug.Log("Here" + Time.deltaTime.ToString());
                    float distance2 = int.MaxValue;
                    for (int i = 0; i < objs.Length; i++)
                    {
                        float dis = Vector3.Distance(objs[i].transform.position, transform.position);
                        if (distance2 > dis)
                        {
                            distance2 = dis;
                            target = objs[i];
                        }
                    }
                }
			}	
		}
	}

    void OnDestroy()
    {
        for(int i=0; i<bulletList.Count; i++){
            Destroy(bulletList[i]);
        }
    }
}
