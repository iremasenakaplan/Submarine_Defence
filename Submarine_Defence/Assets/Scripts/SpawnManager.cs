using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PathCreation; 
using PathCreation.Examples;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] LevelScriptable[] levelConfigs;
    [SerializeField] EnvironmentScriptable[] environmentScriptable;
    [SerializeField] ShipScriptable[] shipConfigs;
    PathCreator[] paths;
    [SerializeField] Material[] skyboxes;
    [SerializeField] GameObject compassBar;
    [SerializeField] GameObject loadEffect;
    //public GameObject enemyPrefab;

   /* [Header("Spawn S�n�rlar�")]
    public Vector2 minXZ = new Vector2(-20f, -20f); 
    public Vector2 maxXZ = new Vector2(20f, 20f);*/

    void Start()
    {
        int envIndex = Random.Range(0, environmentScriptable.Length);
        Instantiate(environmentScriptable[envIndex].sun, environmentScriptable[envIndex].sun.transform.position, environmentScriptable[envIndex].sun.transform.rotation);
        Instantiate(environmentScriptable[envIndex].ground, environmentScriptable[envIndex].ground.transform.position, environmentScriptable[envIndex].ground.transform.rotation);
        RenderSettings.skybox = environmentScriptable[envIndex].skyboxes[Random.Range(0, environmentScriptable[envIndex].skyboxes.Length )];
        
        paths = FindObjectsOfType<PathCreator>();

        Instantiate(shipConfigs[MenuManager.currentGunIndex].gameGun, shipConfigs[MenuManager.currentGunIndex].gameGun.transform.position, shipConfigs[MenuManager.currentGunIndex].gameGun.transform.rotation);
        MechanicsManager.Instance.SetMaxZoom(shipConfigs[MenuManager.currentGunIndex].zoom);
        compassBar.SetActive(true);
        int level = PlayerPrefs.GetInt(Application.identifier + "Level");
        
        for (int i = 0; i < levelConfigs.Length; i++)
        {
            if (levelConfigs[i].startIndex > level)
            {
                //Instantiate(levelConfigs[i - 1].environment, levelConfigs[i - 1].environment.transform.position, levelConfigs[i - 1].environment.transform.rotation);
               // UIManager.Instance.SetKillCount(levelConfigs[i - 1].killLimit);
               // shouldKillCount = levelConfigs[i - 1].killLimit;
                if (levelConfigs[i - 1].isFoggy)
                {
                    RenderSettings.fog = true;
                    RenderSettings.fogMode = FogMode.Exponential;
                }
                StartCoroutine(SpawnEnemies(levelConfigs[i-1]));
                break;
               
            }
            else if (i == levelConfigs.Length - 1)
            {
                //Instantiate(levelConfigs[i].environment, levelConfigs[i].environment.transform.position, levelConfigs[i].environment.transform.rotation);
               // UIManager.Instance.SetKillCount(levelConfigs[i].killLimit);
                //shouldKillCount = levelConfigs[i].killLimit;
                if (levelConfigs[i].isFoggy)
                {
                    RenderSettings.fog = true;
                    RenderSettings.fogMode = FogMode.Exponential;
                }
                StartCoroutine(SpawnEnemies(levelConfigs[i]));
                // SpawnEnemyShips(levelConfigs[i].enemyShips);

                break;
            }
        }

        
        
        

        
    }

    IEnumerator SpawnEnemies(LevelScriptable levelConfig){
        yield return new WaitForSeconds(2);
        Destroy(loadEffect);
        SpawnEnemyShips(levelConfig.enemyShips);
        
    }

    void SpawnEnemyShips(GameObject[] shipList)
    {

        foreach (GameObject ship in shipList)
        {


            Vector2 randXZ = RandomPointInAnnulus(500, 2000);
            Vector3 randomPosition = new Vector3(
            randXZ.x,
            transform.position.y,
            randXZ.y);
 
            GameObject s = Instantiate(ship, randomPosition, ship.transform.rotation);
            s.GetComponent<PathFollower>().SetPath(paths[Random.Range(0, paths.Length-1)]);
            s.GetComponent<PathFollower>().speed += Random.Range(0f, 0.5f);
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
