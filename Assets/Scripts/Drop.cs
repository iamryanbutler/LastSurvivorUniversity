using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    // the prefab of the active power up
    public PowerUp powerUp;

    public enum DropTypes
    {
        Health_Pack,
        Shield_Pack,
        Ammo_Pack,
    }

    /// <summary>
    /// When the player walks into this power up, this executes
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch(powerUp.name)
            {
                case DropTypes.Health_Pack:
                    HealthPack(other.gameObject); break;
                case DropTypes.Shield_Pack:
                    ShieldPack(other.gameObject); break;
                case DropTypes.Ammo_Pack:
                    AmmoPack(other.gameObject); break;
                default:
                    break;
            }
        }
    }

    void HealthPack(GameObject player)
    {
        // don't pick up the shield drop if the player can't use it
        if (!player.GetComponent<playerHealth>().CanApplyHealth())
            return;

        player.GetComponent<playerHealth>().AddHealth(powerUp.changeInHealth);
        Destroy(gameObject);
    }

    void ShieldPack(GameObject player)
    {
        // don't pick up the shield drop if the player can't use it
        if (!player.GetComponent<playerHealth>().CanApplyShield())
            return;

        player.GetComponent<playerHealth>().AddShield(powerUp.changeInShield);
        Destroy(gameObject);
    }

    void AmmoPack(GameObject player)
    {
        // don't pick up the ammo drop if the player has melee weapon out
        /*if (player.GetComponent<Weapon>().loadout[0].isMelee)
            return;*/

        player.GetComponent<Weapon>().loadout[1].IncreaseRemainingTotal(powerUp.changeInTotalAmmo);
        Destroy(gameObject);
    }
}
