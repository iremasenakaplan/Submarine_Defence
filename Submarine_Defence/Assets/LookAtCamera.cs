using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    Transform player;


    private void Start()
    {
        player = Camera.main.transform;
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.forward * 10000);
    }
}
