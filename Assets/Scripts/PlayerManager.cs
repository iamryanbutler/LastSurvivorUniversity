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
    }

    void Start()
    {
        if (currentlyLoadedPlayerInfo != null)
            LoadPlayerInfo(currentlyLoadedPlayerInfo);
        else
            LoadPlayerInfoFirstTime();
    }

    public void LoadPlayerInfoFirstTime()
    {
        instance.player.GetComponent<playerHealth>().SetCurrentShield(instance.player.GetComponent<playerHealth>().fullShield);
        instance.player.GetComponent<playerHealth>().SetCurrentHealth(instance.player.GetComponent<playerHealth>().fullHealth);

        instance.player.GetComponent<Weapon>().currentIndex = 0;
        foreach(Gun gun in instance.player.GetComponent<Weapon>().loadout)
            gun.Initialize();

        Debug.Log("Initalized Player Info");
        Debug.Log($"Shield: {instance.player.GetComponent<playerHealth>().GetCurrentShield()} / {instance.player.GetComponent<playerHealth>().fullShield}");
        Debug.Log($"Health: {instance.player.GetComponent<playerHealth>().GetCurrentHealth()} / {instance.player.GetComponent<playerHealth>().fullHealth}");
    }

    public void LoadPlayerInfo(PlayerInfo storedPlayerInfo)
    {
        instance.player.GetComponent<playerHealth>().SetCurrentShield(storedPlayerInfo.currentShield);
        instance.player.GetComponent<playerHealth>().PlayerShield.fillAmount = (float)storedPlayerInfo.currentShield / (float)storedPlayerInfo.fullShield;
        instance.player.GetComponent<playerHealth>().fullShield = storedPlayerInfo.fullShield;

        instance.player.GetComponent<playerHealth>().SetCurrentHealth(storedPlayerInfo.currentHealth);
        instance.player.GetComponent<playerHealth>().PlayerHealth.fillAmount = (float)storedPlayerInfo.currentHealth / (float)storedPlayerInfo.fullHealth;
        instance.player.GetComponent<playerHealth>().fullHealth = storedPlayerInfo.fullHealth;

        instance.player.GetComponent<Weapon>().currentIndex = storedPlayerInfo.currentWeaponIndex;
        instance.player.GetComponent<Weapon>().loadout[storedPlayerInfo.currentWeaponIndex].Initialize();
        instance.player.GetComponent<Weapon>().loadout[storedPlayerInfo.currentWeaponIndex].SetCurrentClip(storedPlayerInfo.currentClipAmmo);
        instance.player.GetComponent<Weapon>().loadout[storedPlayerInfo.currentWeaponIndex].SetRemainingTotal(storedPlayerInfo.currentTotalAmmo);



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

            currentWeaponIndex = instance.player.GetComponent<Weapon>().currentIndex,
            currentClipAmmo = instance.player.GetComponent<Weapon>().loadout[instance.player.GetComponent<Weapon>().currentIndex].GetCurrentClip(),
            currentTotalAmmo = instance.player.GetComponent<Weapon>().loadout[instance.player.GetComponent<Weapon>().currentIndex].GetRemainingTotal(),
        };
        GlobalController.CurrentInstance.PlayerInfoStorage.SetPlayerInfo(currentPlayerInfo);
    }
}
