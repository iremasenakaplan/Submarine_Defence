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
            UIManager.Instance.Won(); // asena seymuru seviyor :D Seymurda asenanin kocaman
            Debug.Log("Oyun bitti! T�m d��manlar �ld�r�ld�.");
        }
    }

    // Kalan d��man say�s�n� hesaplamak i�in bir metod
    public int GetRemainingEnemyCount()
    {
        return totalEnemies - killedEnemies;
    }
}
