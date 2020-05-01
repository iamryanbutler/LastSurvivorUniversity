using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public float enemyMaxHealth;
   // public GameObject deathFX;

    float currentHealth;

   // public bool drops;
   // public GameObject theDrop;

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
       // Instantiate(deathFX, transform.position, transform.rotation);
        Destroy(gameObject);
       // if (drops) Instantiate(theDrop, transform.position, transform.rotation);
    }
}
