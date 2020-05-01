using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public float enemyMaxHealth;
    
   // public GameObject deathFX;

    float currentHealth;

   // public bool drops;
    public GameObject prefab;
    GameObject theDrop;
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

        Debug.Log(this.gameObject.name);
        if (String.Equals(this.gameObject.name, "BrennerBoss", StringComparison.OrdinalIgnoreCase))
        {
            
            theDrop = Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
            theDrop.transform.position += new Vector3(0, this.gameObject.transform.position.y, 0);
        }
        Destroy(gameObject);
    }
}
