using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    #region VARIABLES
    [Header ("Informations to save")]
    public float playerPosX;
    public float playerPosY;

    public int currentHealth;

    public List<InventorySaveData> inventoryItems = new List<InventorySaveData>();
    #endregion
}

[System.Serializable]
public class InventorySaveData
{
    #region VARIABLES
    [Header ("Inventory identification")]
    public string itemID;
    public int quantity;
    #endregion
}