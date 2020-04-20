using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerUp", menuName = "Power Up")]
public class PowerUp : ScriptableObject
{
    // name of the power up
    public Drop.DropTypes name;

    // amount ( positive or negative ) of change to the player's health.
    public float changeInHealth;

    // amount ( positive or negative ) of change to the player's shield.
    public float changeInShield;

    // amount ( positive or negative ) of change to the player's stamina.
    public float changeInStamina;

    // amount ( positive or negative ) of change to the player's total ammo.
    public int changeInTotalAmmo;
    
    // power up prefab and components
    private GameObject prefab;
}
