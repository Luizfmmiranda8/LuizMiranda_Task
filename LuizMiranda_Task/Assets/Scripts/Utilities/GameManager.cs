using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region EVENTS
    void OnEnable()
    {
        PlayerHealth.OnPlayerDied += HandlePlayerDeath;
    }

    void OnDisable()
    {
        PlayerHealth.OnPlayerDied -= HandlePlayerDeath;
    }
    #endregion

    #region METHODS
    void HandlePlayerDeath()
    {
        StartCoroutine(ReloadFromCheckpoint());
    }

    System.Collections.IEnumerator ReloadFromCheckpoint()
    {
        yield return new WaitForSeconds(1f);

        GameLoader.Instance.LoadGame();
    }
    #endregion
}