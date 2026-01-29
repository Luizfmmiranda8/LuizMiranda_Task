using System.Collections;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    #region VARIABLES
    [Header ("Saved items")]
    [SerializeField] ItemData[] allItems;

    [Header ("Instance")]
    public static GameLoader Instance;
    #endregion

    #region EVENTS
    void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    IEnumerator Start()
    {
        yield return null;

        SaveData data = SaveSystem.LoadGame();

        if (data == null) yield break;

        // Player position
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        player.position = new Vector2(data.playerPosX, data.playerPosY);

        // Health
        PlayerHealth health = FindFirstObjectByType<PlayerHealth>();
        health.SetHealth(data.currentHealth);

        // Inventory
        Inventory.Instance.LoadInventory(data.inventoryItems, allItems);
    }

    public void LoadGame()
    {
        StartCoroutine(LoadRoutine());
    }

    IEnumerator LoadRoutine()
    {
        yield return null;

        SaveData data = SaveSystem.LoadGame();

        if (data == null) yield break;

        // Player position
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        player.position = new Vector2(data.playerPosX, data.playerPosY);

        // Health
        PlayerHealth health = FindFirstObjectByType<PlayerHealth>();
        health.SetHealth(data.currentHealth);

        // Inventory
        Inventory.Instance.LoadInventory(data.inventoryItems, allItems);
    }
    #endregion
}