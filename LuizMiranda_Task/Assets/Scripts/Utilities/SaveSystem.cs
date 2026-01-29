using System.IO;
using UnityEngine;

public static class SaveSystem
{
    #region VARIABLES
    [Header ("Save settings")]
    private static string savePath = Application.persistentDataPath + "/save.json";
    #endregion

    #region METHODS
    public static void SaveGame()
    {
        SaveData data = new SaveData();

        // Player position
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        data.playerPosX = player.position.x;
        data.playerPosY = player.position.y;

        // Player health
        PlayerHealth health = Object.FindFirstObjectByType<PlayerHealth>();
        data.currentHealth = health.GetCurrentHealth();

        // Inventory
        foreach (var slot in Inventory.Instance.items)
        {
            InventorySaveData itemData = new InventorySaveData();
            itemData.itemID = slot.item.itemID;
            itemData.quantity = slot.quantity;

            data.inventoryItems.Add(itemData);
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Game Saved");
    }

    public static SaveData LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("No save file found");
            return null;
        }

        string json = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        return data;
    }

    public static bool HasSave()
    {
        string path = Application.persistentDataPath + "/save.json";
        return System.IO.File.Exists(path);
    }
    #endregion
}