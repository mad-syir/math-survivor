using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HealthSystem healthsys;
    public int maxHealth = 70;
    public int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
        healthsys.SetMaxHealth(maxHealth);
        
        Debug.Log("Press A to Heal, or Space to Damage");
        Debug.Log("Health: " + currentHealth);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage(10);
            Debug.Log("Health is: " + currentHealth);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Healing(10);
            Debug.Log("Health is: " + currentHealth);
        }
    }
    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth < 0)
            currentHealth = 0;
        healthsys.SetHealth(currentHealth);
    }

    //healing might be considered
    public void Healing(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        healthsys.SetHealth(currentHealth);
    }
}
