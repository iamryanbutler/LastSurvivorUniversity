using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class Weapon : MonoBehaviour
{
    // the player's loadout
    public Gun[] loadout;

    // index of the current weapon in the player's loadout
    public int currentIndex;

    // weapons parent ( Player body )
    public Transform weaponParent;

    // timer until next shot can be fired
    private float currentCooldown;

    // the weapon that the player is holding
    private GameObject currentWeapon;

    public TextMeshProUGUI weaponNameHUD;
    public TextMeshProUGUI weaponAmmoHUD;

    // is the gun reloading
    private bool isReloading;

    Animator anim;

    /// <summary>
    /// Initilizes each gun in the player's loadout
    /// Equips the first gun in the loadout
    /// </summary>
    void Start() {
        anim = GetComponent<Animator>();
        Equip(currentIndex);
    }

    /// <summary>
    /// Controls player input related to Weapons.
    /// </summary>
    void Update() {
        // Pressing 1 equips loadout[0]
        //if(Input.GetKeyDown(KeyCode.Alpha1)) Equip(0);

        // Pressing 2 equips loadout[1]
        if (Input.GetKeyDown(KeyCode.Alpha2)) NextWeapon();

        if (Input.GetKeyDown(KeyCode.Alpha7)) DialogueManager.instance.PushDialogue(new List<string> { "This is the text that would play for option 1",
                                                                                                                    "This is the text that would play for option 2",
                                                                                                                    "This is the text that would play for option 3",
                                                                                                                    "This is the text that would play for option 4",
                                                                                                                    "This is the text that would play for option 5",
                                                                                                                    "This is the text that would play for option 6"});

        //if (Input.GetKeyDown(KeyCode.Alpha8)) DialogueManager.instance.ReplayPrevious();

        if (currentWeapon != null)
        {
            // Shooting functionality for non full-auto guns
            if (loadout[currentIndex].fireType != 2)
            {
                if (Input.GetKeyDown(KeyCode.Space) && currentCooldown <= 0)
                {
                    // returns true only if it has a bullet in the clip
                    // auto-decrements the bullet from the gun ammo
                    if (loadout[currentIndex].FireBullet() || loadout[currentIndex].isMelee)
                    {
                        PlayAnimation();
                        // generate the bullet projectile
                        Shoot();
                        PlaySound();
                    }
                    else
                    {
                        if (loadout[currentIndex].GetRemainingTotal() > 0)
                            //begin reload
                            StartCoroutine(Reload(loadout[currentIndex].reloadTime));
                        else
                            Debug.Log("Out of Ammo!");
                    }
                }
            }
            // Shooting functionality for full-auto guns
            else
            {
                if (Input.GetKey(KeyCode.Space) && currentCooldown <= 0)
                {
                    // returns true only if it has a bullet in the clip
                    // auto-decrements the bullet from the gun ammo
                    if (loadout[currentIndex].FireBullet() || loadout[currentIndex].isMelee)
                    {
                        PlayAnimation();
                        // generate the bullet projectile
                        Shoot();
                        PlaySound();
                    }
                    else
                    {
                        if (loadout[currentIndex].GetRemainingTotal() > 0)
                            //need to reload
                            StartCoroutine(Reload(loadout[currentIndex].reloadTime));
                        else
                            Debug.Log("Out of Ammo!");
                    }
                }
            }

            // Pressing R will attempt to manually reload the weapon.
            if (Input.GetKeyDown(KeyCode.R) && loadout[currentIndex].GetCurrentClip() < loadout[currentIndex].totalclipCapacity)
            {
                if (loadout[currentIndex].GetRemainingTotal() > 0)
                    StartCoroutine(Reload(loadout[currentIndex].reloadTime));
                else
                    Debug.Log("No more reserved ammo");
            }


            // Manage cooldown for fire rate.
            if (currentCooldown > 0)
                currentCooldown -= Time.deltaTime;

            // create an "elastic" effect. If the gun moves, it will always go back to the start position.
            currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 4f);
        }
    }

    void LateUpdate()
    {
        weaponNameHUD.text = loadout[currentIndex].name;

        if (isReloading)
        {
            weaponAmmoHUD.color = new Color32(255, 0, 0, 255); //red
            weaponAmmoHUD.text = "Reloading";
        }
        else if ((loadout[currentIndex].GetRemainingTotal() == 0 && loadout[currentIndex].GetCurrentClip() == 0) && !loadout[currentIndex].isMelee)
        {
            weaponAmmoHUD.color = new Color32(255, 0, 0, 255); //red
            weaponAmmoHUD.text = "Out of Ammo";
        }
        else
        {
            weaponAmmoHUD.color = new Color32(70, 29, 124, 255); //purple
            weaponAmmoHUD.text = !loadout[currentIndex].isMelee ? $"{loadout[currentIndex].GetCurrentClip()} / {loadout[currentIndex].GetRemainingTotal()}" : "";
        }
    }

    /// <summary>
    /// Activate Reload trigger and wait.
    /// </summary>
    IEnumerator Reload(float p_wait)
    {
        isReloading = true;
        currentWeapon.SetActive(false);
        yield return new WaitForSeconds(p_wait);

        loadout[currentIndex].Reload();
        currentWeapon.SetActive(true);
        isReloading = false;
    }

    /// <summary>
    /// Equip the specified gun from loadout given p_ind.
    /// </summary>
    /// <param name"p_ind">Index of weapon in player loadout</param>
    void Equip(int p_ind) {
        if (currentWeapon != null) {
            if (isReloading)
                StopCoroutine("Reload");
            Destroy(currentWeapon);
        }

        GameObject t_newWeapon = Instantiate(loadout[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
        t_newWeapon.transform.localPosition = Vector3.zero;
        t_newWeapon.transform.localEulerAngles = Vector3.zero;

        currentIndex = p_ind;
        currentWeapon = t_newWeapon;
    }

    public void NextWeapon() { Equip(++currentIndex);
        FindObjectOfType<AudioManager>().Play("weaponPickup");
    }


    /// <summary>
    /// Generate the bullet projectile for the gun's shot.
    /// </summary>
    void Shoot()
    {
        // gun's point of fire
        Transform t_spawn = transform.Find("ShootPosition");

        // create the projectile at the gun's point of fire.
        GameObject t_bullet = Instantiate(loadout[currentIndex].bulletPrefab, t_spawn.position, t_spawn.rotation) as GameObject;
        
        // t_bullet.transform.localPosition = Vector3.zero;
        // t_bullet.transform.localEulerAngles = Vector3.zero;

        // Gives the bullet all the properties it needs to become a projectile.
        t_bullet.GetComponent<Bullet>().Setup(loadout[currentIndex].bulletLifespan, loadout[currentIndex].bulletSpeed, loadout[currentIndex].damage);   

        // gun fx ( recoil & knockback )
        currentWeapon.transform.Rotate(-loadout[currentIndex].recoil, 0, 0);
        currentWeapon.transform.position -= currentWeapon.transform.forward * -loadout[currentIndex].kickback;

        // Set cooldown for managing fire rate.
        currentCooldown = loadout[currentIndex].fireRate;
    }

    void PlayAnimation()
    {
        switch (loadout[currentIndex].isMelee)
        {
            case true:
                anim.SetTrigger("isMelee");
                break;
            case false:
                anim.SetTrigger("Shooting");
                break;
        }
    }

    void PlaySound()
    {
        switch (loadout[currentIndex].isMelee)
        {
            case true:
                FindObjectOfType<AudioManager>().Play("melee");
                break;
            case false:
                FindObjectOfType<AudioManager>().Play("gun");
                break;
        }
    }

    public bool CanApplyAmmo() => loadout[currentIndex].isMelee ? false : true;
}
