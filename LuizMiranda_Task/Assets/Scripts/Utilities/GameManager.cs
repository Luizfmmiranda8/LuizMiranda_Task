using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region EVENTS
    void OnEnable()
    {
        PlayerHealth.OnPlayerDied += HandlePlayerDeath;
        FinishGame.OnFinishGameRequest += HandleFinishGame;
    }

    void OnDisable()
    {
        PlayerHealth.OnPlayerDied -= HandlePlayerDeath;
        FinishGame.OnFinishGameRequest -= HandleFinishGame;
    }
    #endregion

    #region METHODS
    void HandlePlayerDeath()
    {
        StartCoroutine(ReloadFromCheckpoint());
    }

    void HandleFinishGame()
    {
        if(SaveSystem.HasSave())
        {
            SaveSystem.DeleteSave();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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