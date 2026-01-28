using UnityEngine;

public class FruitPickup : MonoBehaviour
{
    #region VARIABLES
    [Header ("Inventory Data")]
    [SerializeField] ItemData itemData;
    #endregion

    #region EVENTS
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;

        PickupItem();
    }
    #endregion

    #region METHODS
    void PickupItem()
    {
        Inventory.Instance.AddItem(itemData);
        Destroy(gameObject);
    }
    #endregion
}
