using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    #region VARIABLES
    [Header ("Itens Settings")]
    public string itemName;
    public Sprite icon;
    public int healAmount;
    #endregion

    #region EVENTS
    #endregion

    #region METHODS
    #endregion
}
