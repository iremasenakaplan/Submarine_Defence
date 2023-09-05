using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] LevelScriptable[] levelConfigs;
    [SerializeField] ShipScriptable[] shipConfigs;
    //public GameObject enemyPrefab;

   /* [Header("Spawn S�n�rlar�")]
    public Vector2 minXZ = new Vector2(-20f, -20f); 
    public Vector2 maxXZ = new Vector2(20f, 20f);*/

    void Start()
    {

        Instantiate(shipConfigs[MenuManager.currentGunIndex].gameGun, shipConfigs[MenuManager.currentGunIndex].gameGun.transform.position, shipConfigs[MenuManager.currentGunIndex].gameGun.transform.rotation);
        int level = PlayerPrefs.GetInt(Application.identifier + "Level");
        
        for (int i = 0; i < levelConfigs.Length; i++)
        {
            if (levelConfigs[i].startIndex > level)
            {
                Instantiate(levelConfigs[i - 1].environment, levelConfigs[i - 1].environment.transform.position, levelConfigs[i - 1].environment.transform.rotation);
               // UIManager.Instance.SetKillCount(levelConfigs[i - 1].killLimit);
               // shouldKillCount = levelConfigs[i - 1].killLimit;
                if (levelConfigs[i - 1].isFoggy)
                {
                    RenderSettings.fog = true;
                    RenderSettings.fogMode = FogMode.Exponential;
                }
                SpawnEnemyShips(levelConfigs[i - 1].enemyShips);
                break;
               
            }
            else if (i == levelConfigs.Length - 1)
            {
                Instantiate(levelConfigs[i].environment, levelConfigs[i].environment.transform.position, levelConfigs[i].environment.transform.rotation);
               // UIManager.Instance.SetKillCount(levelConfigs[i].killLimit);
                //shouldKillCount = levelConfigs[i].killLimit;
                if (levelConfigs[i].isFoggy)
                {
                    RenderSettings.fog = true;
                    RenderSettings.fogMode = FogMode.Exponential;
                }
                SpawnEnemyShips(levelConfigs[i].enemyShips);

                break;
            }
        }

        
        
    }

    void SpawnEnemyShips(GameObject[] shipList)
    {

        foreach (GameObject ship in shipList)
        {


            Vector2 randXZ = RandomPointInAnnulus(50, 200);
            Vector3 randomPosition = new Vector3(
            randXZ.x,
            transform.position.y,
            randXZ.y);
 
            Instantiate(ship, randomPosition, ship.transform.rotation);
        }
        GameManager.Instance.SetEnemyCount(shipList.Length); 
    }


    public Vector2 RandomPointInAnnulus(float minRadius, float maxRadius)
    {

        var randomDirection = (Random.insideUnitCircle ).normalized;

        var randomDistance = Random.Range(minRadius, maxRadius);

        var point = randomDirection * randomDistance;
        Debug.Log(point + "<-POINT");

        return point;
    }
}
