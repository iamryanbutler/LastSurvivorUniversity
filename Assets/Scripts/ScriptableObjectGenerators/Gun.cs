using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    // name of the gun
    public string name;

    // amount of damage the gun does per hit
    public int damage;

    // number of bullets gun can hold in one clip
    public int totalclipCapacity;

    // total ammo for the gun ( this includes the clip )
    public int totalAmmo;

    // the type of fire style
    // 1 semi | 2 full-auto | 3+ burst size
    public int fireType;

    // rate at which consecutive shots can be made
    public float fireRate;

    // randomized alteration of bullet fire point
    public float bloom;

    // randomized motion of gun after a shot
    public float recoil;

    // amount to kick the gun backwards after a shot
    public float kickback;

    // time it takes to reload this gun
    public float reloadTime;

    // time the bullet will survive in the scene without hitting anything
    public float bulletLifespan;

    // speed that the bullet will travel at
    public float bulletSpeed;
    
    // gun prefab and components
    public GameObject prefab;

    // bullet projectile prefab
    public GameObject bulletPrefab;

    public bool isMelee;

    // current total ammo count ( what's left )
    private int currentTotalAmmo;

    // current ammo count in clip
    private int currentClipAmmo;

    /// <summary>
    /// fills the current clip and reserved ammo
    /// </summary>
    /// <returns></returns>
    public void Initialize()
    {
        currentTotalAmmo = totalAmmo - totalclipCapacity;
        currentClipAmmo = totalclipCapacity;
    }

    /// <summary>
    /// If a bullet is available in the clip, shoot the bullet.
    /// </summary>
    /// <returns>A bool representing whether a bullet is shootable</returns>
    public bool FireBullet()
    {
        if(currentClipAmmo > 0){
            currentClipAmmo--;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Reloads the gun.
    /// </summary>
    public void Reload()
    {
        currentTotalAmmo += currentClipAmmo;
        currentClipAmmo = Mathf.Min(totalclipCapacity, currentTotalAmmo);
        currentTotalAmmo -= currentClipAmmo;
    }

    /// <summary>
    /// Gets the current total ammo left in total.
    /// </summary>
    /// <returns>current total ammo count ( what's left )</returns>
    public int GetRemainingTotal()
    {
        return currentTotalAmmo;
    }

    /// <summary>
    /// Increases the current total ammo left in total.
    /// </summary>
    public void IncreaseRemainingTotal(int amount)
    {
        currentTotalAmmo += amount;
    }

    /// <summary>
    /// Gets the current ammo count in the clip.
    /// </summary>
    /// <returns>current ammo count in clip</returns>
    public int GetCurrentClip()
    {
        return currentClipAmmo;
    }
}
