using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // time the bullet will survive in the scene without hitting anything
    public float bulletLifespan;

    // speed that the bullet will travel at
    public float bulletSpeed;

    // amount of damage the gun does per hit
    public float bulletDamage;

    /// <summary>
    /// Gives the bullet all the properties it needs to become a projectile.
    /// </summary>
    /// <param name="bulletLifespan"></param>
    /// <param name="bulletSpeed"></param>
    /// <param name="bulletDamage"></param>
    public void Setup(float bulletLifespan, float bulletSpeed, float bulletDamage) 
    {
        this.bulletLifespan = bulletLifespan;
        this.bulletSpeed = bulletSpeed;
        this.bulletDamage = bulletDamage;
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(gameObject.transform.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(gameObject, bulletLifespan);
    }

    /// <summary>
    /// This event is run if the bullet collides with another collider.
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter(Collider collider)
    {
        // If the collision was with an enemy
        if(collider.CompareTag("Enemy"))
        {
            // Deal damage to enemy
            enemyHealth hurtEnemy = collider.gameObject.GetComponent<enemyHealth>();
            hurtEnemy.addDamage(bulletDamage);
            Debug.Log("Damaged Player with " + bulletDamage);
        }
        
        Destroy(gameObject);
    }
}
