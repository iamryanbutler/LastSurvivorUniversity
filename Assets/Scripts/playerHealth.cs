﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    

    public float fullHealth;
    public float fullShield;
    //public float fullStamina;
    //public GameObject deathFX;

    float currentHealth;
    float currentShield;

    

    //HUD variables
    public Image PlayerHealth;
    public Image PlayerShield;
    //public Image PlayerStamina;
    public Image damageScreen;
    //public Text gameOverScreen;
    
    
    bool damaged = false;
    Color damagedColor = new Color(0f, 0f, 0f, 0.5f);
    float smoothColour = 5f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = fullHealth;
        currentShield = fullShield;
        //currentStamina = fullStamina;
        damaged = false;
    }

    public void AddHealth(float health)
    {
        if (health == 0) return;

        if (CanApplyHealth())
        {
            currentHealth += health;
            if (currentHealth > fullHealth) currentHealth = fullHealth;
            PlayerHealth.fillAmount = (float)currentHealth / (float)fullHealth;
        }
    }

    public void AddShield(float Shield)
    {
        if (Shield == 0) return;

        if (CanApplyShield())
        {
            currentShield += Shield;
            if (currentShield > fullHealth) currentShield = fullShield;
            PlayerShield.fillAmount = (float)currentShield / (float)fullShield;
        }
    }

    /*public void AddStamina(float stamina)
    {
        if (stamina == 0) return;

        if (CanApplyStamina())
        {
            currentStamina += stamina;
            if (currentStamina > fullStamina) currentStamina = fullStamina;
            PlayerStamina.fillAmount = (float)currentStamina / (float)fullStamina;
        }
    }*/


    public void addDamage(float damage)
    {
        if (damage <= 0) return;

        //Check statement if health or shield will take damage
        if (currentShield <= 0)
        {
            currentHealth -= damage;
            PlayerHealth.fillAmount = (float)currentHealth / (float)fullHealth;
        }
        else
        {
            currentShield -= damage;
            PlayerShield.fillAmount = (float)currentShield / (float)fullShield;
        }

        if (currentHealth <= 0) { makeDead(); }
        damaged = true;
    }

    public void makeDead() => Destroy(gameObject);
    public bool CanApplyHealth() => (currentHealth < fullHealth) ? true : false;
    public bool CanApplyShield() => (currentShield < fullShield) ? true : false;
    //public bool CanApplyStamina() => (currentStamina < fullStamina) ? true : false;

    public float GetCurrentHealth() => currentHealth;
    public float SetCurrentHealth(float health) => currentHealth = health;
    public float GetCurrentShield() => currentShield;
    public float SetCurrentShield(float shield) => currentShield = shield;

}