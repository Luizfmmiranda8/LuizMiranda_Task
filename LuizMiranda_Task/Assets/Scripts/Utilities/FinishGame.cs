using UnityEngine;
using System;

public class FinishGame : MonoBehaviour
{
    #region VARIABLES
    [Header ("SFX")]
    [SerializeField] AudioClip finishGameSFX;
    #endregion
    #region EVENTS
    public static event Action OnFinishGameRequest;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;

        AudioSource.PlayClipAtPoint(finishGameSFX, transform.position);
        OnFinishGameRequest?.Invoke();
    }
    #endregion
}
