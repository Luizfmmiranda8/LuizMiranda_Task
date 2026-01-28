using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region VARIABLES
    public static Inventory Instance;

    public List<InventorySlot> items = new List<InventorySlot>();
    public static event Action OnInventoryChanged;

    PlayerHealth playerHealth;

    private int selectedIndex = 0;
    #endregion

    #region EVENTS
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }
    #endregion

    #region METHODS

    public void AddItem(ItemData item)
    {
        InventorySlot existing = items.Find(i => i.item == item);

        if (existing != null)
            existing.quantity++;
        else
            items.Add(new InventorySlot(item));

        OnInventoryChanged?.Invoke();
    }

    public void NextItem()
    {
        if (items.Count == 0) return;

        selectedIndex++;

        if (selectedIndex >= items.Count)
            selectedIndex = 0;

        OnInventoryChanged?.Invoke();
    }

    public void UseSelectedItem()
    {
        if (items.Count == 0) return;

        InventorySlot slot = items[selectedIndex];

        playerHealth.Heal(slot.item.healAmount);

        slot.quantity--;

        if (slot.quantity <= 0)
        {
            items.RemoveAt(selectedIndex);

            if (selectedIndex >= items.Count)
                selectedIndex = 0;
        }

        OnInventoryChanged?.Invoke();
    }

    public int GetSelectedIndex()
    {
        return selectedIndex;
    }

    #endregion
}

[System.Serializable]
public class InventorySlot
{
    public ItemData item;
    public int quantity;

    public InventorySlot(ItemData item)
    {
        this.item = item;
        quantity = 1;
    }
}