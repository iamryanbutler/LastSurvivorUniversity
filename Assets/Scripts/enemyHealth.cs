using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public float enemyMaxHealth;
    // public GameObject deathFX;

    float currentHealth;

    // public bool drops;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) makeDead();

    }

    void makeDead()
    {
        Destroy(gameObject);
        if (String.Equals(this.gameObject.name, "BrennerBoss", StringComparison.OrdinalIgnoreCase))
            player.GetComponent<Weapon>().NextWeapon();
    }
}
