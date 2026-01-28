using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    #region VARIABLES
    [Header("Inventory Settings")]
    [SerializeField] Image[] icons;
    [SerializeField] TextMeshProUGUI[] quantities;
    [SerializeField] GameObject[] highlights;
    #endregion

    #region EVENTS
    void OnEnable()
    {
        Inventory.OnInventoryChanged += UpdateUI;
    }

    void OnDisable()
    {
        Inventory.OnInventoryChanged -= UpdateUI;
    }

    void Start()
    {
        UpdateUI();
    }
    #endregion

    #region METHODS
    void UpdateUI()
    {
        if (Inventory.Instance == null) return;

        var inventory = Inventory.Instance;
        var items = inventory.items;
        int selectedIndex = inventory.GetSelectedIndex();

        for (int i = 0; i < icons.Length; i++)
        {
            if (i < items.Count)
            {
                icons[i].sprite = items[i].item.icon;
                icons[i].enabled = true;

                quantities[i].text = items[i].quantity.ToString();
                quantities[i].gameObject.SetActive(items[i].quantity > 1);
            }
            else
            {
                icons[i].enabled = false;
                quantities[i].gameObject.SetActive(false);
            }

            highlights[i].SetActive(i == selectedIndex && i < items.Count);
        }
    }

    #endregion
}