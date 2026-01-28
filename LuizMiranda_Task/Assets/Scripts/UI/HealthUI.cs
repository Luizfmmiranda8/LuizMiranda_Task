using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    #region VARIABLES
    [Header ("Images")]
    [SerializeField] Image[] hearts;
    #endregion

    #region EVENTS
    void OnEnable()
    {
        PlayerHealth.onHealthChanged += UpdateHearts;
    }

    void Osable()
    {
        PlayerHealth.onHealthChanged -= UpdateHearts;
    }
    #endregion

    #region METHODS
    void UpdateHearts(int currentHealth, int maxHealth)
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentHealth;
        }
    }
    #endregion
}
