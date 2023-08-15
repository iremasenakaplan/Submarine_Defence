using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick; // Joystick referansý
    public float rotationSpeed = 10f; // Oyuncunun dönme hýzý

    void Update()
    {
        // Joystick'in yatay ve dikey eksendeki deðerlerini al
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // Joystick deðerleriyle oyuncuyu döndür
        // Eðer yatay giriþ pozitifse saða, negatifse sola dönecektir
        // Eðer dikey giriþ pozitifse yukarý, negatifse aþaðý dönecektir
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.right, verticalInput * rotationSpeed * Time.deltaTime);
    }

}
