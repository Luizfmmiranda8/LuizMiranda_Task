using UnityEngine;

public class FruitPickup : MonoBehaviour
{
    #region VARIABLES
    [Header ("Inventory Data")]
    [SerializeField] ItemData itemData;

    [Header ("Verify state")]
    bool wasCollected = false;

    [Header ("SFX")]
    [SerializeField] AudioClip fruitPickupSFX;
    #endregion

    #region EVENTS
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;

        if(!wasCollected)
        {
            PickupItem();
            wasCollected = true;
        }
    }
    #endregion

    #region METHODS
    void PickupItem()
    {
        AudioSource.PlayClipAtPoint(fruitPickupSFX, transform.position);
        Inventory.Instance.AddItem(itemData);
        Destroy(gameObject);
    }
    #endregion
}
