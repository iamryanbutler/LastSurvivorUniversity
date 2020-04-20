using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public GameObject player;
    PlayerInfo currentlyLoadedPlayerInfo;

    void Awake()
    {
        instance = this;
        currentlyLoadedPlayerInfo = GlobalController.CurrentInstance.PlayerInfoStorage.GetPlayerInfo();
        if (currentlyLoadedPlayerInfo != null)
            LoadPlayerInfo(currentlyLoadedPlayerInfo);
    }

    public void LoadPlayerInfo(PlayerInfo storedPlayerInfo)
    {
        instance.player.GetComponent<playerHealth>().SetCurrentHealth(storedPlayerInfo.currentHealth);
        instance.player.GetComponent<playerHealth>().fullHealth = storedPlayerInfo.fullHealth;

        instance.player.GetComponent<playerHealth>().SetCurrentShield(storedPlayerInfo.currentShield);
        instance.player.GetComponent<playerHealth>().fullShield = storedPlayerInfo.fullShield;

        instance.player.GetComponent<Weapon>().loadout = storedPlayerInfo.loadout;

        Debug.Log("Loaded Player Info");
        Debug.Log($"Shield: {instance.player.GetComponent<playerHealth>().GetCurrentShield()} / {instance.player.GetComponent<playerHealth>().fullShield}");
        Debug.Log($"Health: {instance.player.GetComponent<playerHealth>().GetCurrentHealth()} / {instance.player.GetComponent<playerHealth>().fullHealth}");
    }

    public static void SavePlayerInfo()
    {
        PlayerInfo currentPlayerInfo = new PlayerInfo()
        {
            currentHealth = instance.player.GetComponent<playerHealth>().GetCurrentHealth(),
            fullHealth = instance.player.GetComponent<playerHealth>().fullHealth,

            currentShield = instance.player.GetComponent<playerHealth>().GetCurrentShield(),
            fullShield = instance.player.GetComponent<playerHealth>().fullShield,

            loadout = instance.player.GetComponent<Weapon>().loadout,
        };
        GlobalController.CurrentInstance.PlayerInfoStorage.SetPlayerInfo(currentPlayerInfo);
    }
}
