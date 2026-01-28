using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region VARIABLES
    [Header ("Health")]
    public int maxHealth = 3;
    public int currentHealth {get; private set;}
    public static event Action<int,int> onHealthChanged;
    #endregion

    #region EVENTS
    void Start()
    {
        currentHealth = maxHealth;
        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    #endregion

    #region METHODS
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        onHealthChanged?.Invoke(currentHealth, maxHealth);

        if(currentHealth <= 0) Die();
    }

    void Die()
    {
        //TODO: Game over
        Debug.Log("Morreu");
    }
    #endregion
}
