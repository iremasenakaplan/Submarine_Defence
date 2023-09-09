using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpactPool : MonoBehaviour
{
    public static BulletImpactPool Instance;
    [SerializeField] private GameObject enemyBulletHitGround;
    [SerializeField] private GameObject playerBulletHitGround;
    [SerializeField] private GameObject bulletHitMetal;
    [SerializeField] private GameObject bulletHitWater;
    [SerializeField] private GameObject bulletHitEnemy;

    private List<GameObject> enemyBulletHitGroundList; 
    private List<GameObject> playerBulletHitGroundList; 
    private List<GameObject> bulletHitMetalList; 
    private List<GameObject> bulletHitWaterList; 
    private List<GameObject> bulletHitEnemyList; 
    int indexEnemyGround = 0;
    int indexPlayerGround = 0;
    int indexMetal = 0;
    int indexWater = 0;
    int indexHitEnemy = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
            Instance = this;


        enemyBulletHitGroundList = new List<GameObject>();
        for(int i=0; i<20; i++){
            GameObject go = Instantiate(enemyBulletHitGround, new Vector3(0,0,0), enemyBulletHitGround.transform.rotation);
            go.SetActive(false);
            enemyBulletHitGroundList.Add(go);
        }


        playerBulletHitGroundList = new List<GameObject>();
        for(int i=0; i<40; i++){
            GameObject go = Instantiate(playerBulletHitGround, new Vector3(0,0,0), playerBulletHitGround.transform.rotation);
            go.SetActive(false);
            playerBulletHitGroundList.Add(go);
        }

        bulletHitMetalList = new List<GameObject>();
        for(int i=0; i<20; i++){
            GameObject go = Instantiate(bulletHitMetal, new Vector3(0,0,0), bulletHitMetal.transform.rotation);
            go.SetActive(false);
            bulletHitMetalList.Add(go);
        }

        bulletHitWaterList = new List<GameObject>();
        for(int i=0; i<20; i++){
            GameObject go = Instantiate(bulletHitWater, new Vector3(0,0,0), bulletHitWater.transform.rotation);
            go.SetActive(false);
            bulletHitWaterList.Add(go);
        }

        bulletHitEnemyList = new List<GameObject>();
        for(int i=0; i<10; i++){
            GameObject go = Instantiate(bulletHitEnemy, new Vector3(0,0,0), bulletHitEnemy.transform.rotation);
            go.SetActive(false);
            bulletHitEnemyList.Add(go);
        }
    }


    public GameObject GetBulletHitGround(){
        indexPlayerGround++;
        if(indexPlayerGround>=playerBulletHitGroundList.Count)
            indexPlayerGround = 0;
        playerBulletHitGroundList[indexPlayerGround].SetActive(true);
        return playerBulletHitGroundList[indexPlayerGround];
    }


    public GameObject GetBulletHitWater(){
        indexWater++;
        if(indexWater>=bulletHitWaterList.Count)
            indexWater = 0;
        bulletHitWaterList[indexWater].SetActive(true);
        return bulletHitWaterList[indexWater];
    }


    public GameObject GetBulletHitMetal(){
        indexMetal++;
        if(indexMetal>=bulletHitMetalList.Count)
            indexMetal = 0;
        bulletHitMetalList[indexMetal].SetActive(true);
        return bulletHitMetalList[indexMetal];
    }


    public GameObject GetBulletHitEnemy(){
        indexHitEnemy++;
        if(indexHitEnemy>=bulletHitEnemyList.Count)
            indexHitEnemy = 0;
        bulletHitEnemyList[indexHitEnemy].SetActive(true);
        return bulletHitEnemyList[indexHitEnemy];
    }
}
