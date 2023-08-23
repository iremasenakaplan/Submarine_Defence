using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleshipHealthBar : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Slider healthSlider; 
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI(); 
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }

        UpdateHealthUI(); // Her hasar al�nd���nda UI'y� g�ncelle
    }

    private void UpdateHealthUI()
    {
        healthSlider.value = (float)currentHealth / maxHealth; // Slider de�erini g�ncelle
    }

    private void Die()
    {
        Debug.Log("Gem destroyed!");
        Destroy(gameObject);
    }
}
