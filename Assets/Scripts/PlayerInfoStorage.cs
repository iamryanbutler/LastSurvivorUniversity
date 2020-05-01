using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoStorage
{
    /// The PlayerInfo data that this PlayerInfoStorageReferences.
    private PlayerInfo myPlayer;

    /// <summary>
    /// Constructor ( Attaches the PlayerInfo to this Player Storage ).
    /// </summary>
    //public PlayerInfoStorage() => myPlayer = new PlayerInfo();

    /// <summary>
    /// Sets the PlayerInfo reference to a new PlayerInfo data object.
    /// </summary>
    public void ResetPlayerInfo() => myPlayer = new PlayerInfo();


    /// <summary>
    /// Gets the PlayerInfo data that is currently saved in the PlayerInfoStorage.
    /// </summary>
    /// <returns>The PlayerInfo data that the PlayerInfoStorage is referencing.</returns>
    public PlayerInfo GetPlayerInfo() => myPlayer;

    /// <summary>
    /// Sets the reference of the passed PlayerInfo data into the PlayerInfoStorage.
    /// </summary>
    /// <param name="info">The PlayerInfo to save to the PlayerInfoStorage</param>
    public void SetPlayerInfo(PlayerInfo info) => myPlayer = info;
}

public class PlayerInfo
{
    public float currentHealth;
    public float fullHealth;
    public float currentShield;
    public float fullShield;
    public int currentWeaponIndex;
    public int currentClipAmmo;
    public int currentTotalAmmo;
}