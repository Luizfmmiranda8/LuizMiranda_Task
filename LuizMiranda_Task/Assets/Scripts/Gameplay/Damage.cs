using UnityEngine;

public class Damage : MonoBehaviour
{
    #region VARIABLES
    [Header ("Player Reference")]
    PlayerHealth playerHealth;

    [Header ("Traps Settings")]
    [SerializeField] int trapDamage = 1;
    #endregion

    #region EVENTS
    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;

        DamagePlayer();
    }
    #endregion

    #region METHODS
    void DamagePlayer()
    {
        playerHealth.TakeDamage(trapDamage);
    }
    #endregion
}
