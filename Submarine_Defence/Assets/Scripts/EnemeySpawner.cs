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
        DusmanOlustur();
    }

    void DusmanOlustur()
    {
        int dusmanSayisi = 5; // Kaç düþman oluþturulacak

        for (int i = 0; i < dusmanSayisi; i++)
        {
            // Rastgele bir pozisyon oluþturun.
            Vector3 rastgelePozisyon = new Vector3(
                Random.Range(minXZ.x, maxXZ.x),
                transform.position.y, // Y ekseni sabit
                Random.Range(minXZ.y, maxXZ.y)
            );

            // Düþmaný oluþturun ve rastgele pozisyona yerleþtirin.
            Instantiate(enemyPrefab, rastgelePozisyon, Quaternion.identity);
        }
    }
}
