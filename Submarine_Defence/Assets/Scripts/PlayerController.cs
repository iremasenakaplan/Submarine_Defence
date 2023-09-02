using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick; // Joystick referansý
    public float rotationSpeed = 10f; // Oyuncunun dönme hýzý

    void Update()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        Camera.main.transform.Rotate(Vector3.right, -verticalInput * rotationSpeed * Time.deltaTime);
    }

}
