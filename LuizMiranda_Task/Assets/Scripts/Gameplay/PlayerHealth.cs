using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region VARIABLES
    [Header ("Health")]
    public int maxHealth = 3;
    public int currentHealth {get; private set;}
    public static event Action<int,int> onHealthChanged;
    bool canTakeDamage = true;

    [Header ("Animations")]
    Animator playerAnimator;
    #endregion

    #region EVENTS
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void EndInvulnerability()
    {
        canTakeDamage = true;
    }
    #endregion

    #region METHODS
    public void TakeDamage(int amount)
    {
        if(canTakeDamage)
        {
            canTakeDamage = false;
            playerAnimator.SetTrigger("Damage");
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            onHealthChanged?.Invoke(currentHealth, maxHealth);

            if(currentHealth <= 0) Die();
        }
    }

    public void Heal(int amount)
    {
        if(amount <= 0) return;

        currentHealth += amount;

        if(currentHealth > maxHealth)
            currentHealth = maxHealth;
        
        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    void Die()
    {
        //TODO: Game over
        Debug.Log("Morreu");
    }
    #endregion
}
