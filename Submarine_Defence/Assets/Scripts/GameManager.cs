using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    int totalEnemies ;
    int killedEnemies = 0;

    private void Start()
    {
        Instance = this;
        
    }

    public void SetEnemyCount(int enemyCount) {
        totalEnemies = enemyCount;
        UIManager.Instance.UpdateEnemyCount(killedEnemies, totalEnemies);
    }
    public void EnemyKilled()
    {
        killedEnemies++;
        UIManager.Instance.UpdateEnemyCount(killedEnemies, totalEnemies);
        if (killedEnemies >= totalEnemies)
        {
            UIManager.Instance.Won(); 
            Debug.Log("Oyun bitti! Tüm düþmanlar öldürüldü.");
        }
    }

    
    public int GetRemainingEnemyCount()
    {
        return totalEnemies - killedEnemies;
    }
}
