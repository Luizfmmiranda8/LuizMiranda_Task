using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (SaveSystem.HasSave())
        {
            GameLoader.Instance.LoadGame();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    #endregion
}