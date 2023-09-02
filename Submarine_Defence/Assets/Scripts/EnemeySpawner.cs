using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Spawn Sýnýrlarý")]
    public Vector2 minXZ = new Vector2(-20f, -20f); 
    public Vector2 maxXZ = new Vector2(20f, 20f);

    void Start()
    {
        spawnEnemy();
    }

    void spawnEnemy()
    {
        int spawnOfEnemies = 3; 

        for (int i = 0; i < spawnOfEnemies; i++)
        {
            
            Vector3 randomPosition = new Vector3(
            Random.Range(minXZ.x, maxXZ.x),
            transform.position.y, 
            Random.Range(minXZ.y, maxXZ.y));
 
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }
}
