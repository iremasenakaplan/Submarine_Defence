using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick; // Joystick referans�
    public float rotationSpeed = 10f; // Oyuncunun d�nme h�z�

    void Update()
    {
        // Joystick'in yatay ve dikey eksendeki de�erlerini al
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // Joystick de�erleriyle oyuncuyu d�nd�r
        // E�er yatay giri� pozitifse sa�a, negatifse sola d�necektir
        // E�er dikey giri� pozitifse yukar�, negatifse a�a�� d�necektir
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.right, verticalInput * rotationSpeed * Time.deltaTime);
    }

}
