using UnityEngine;
using System;

public class FinishGame : MonoBehaviour
{
    #region EVENTS
    public static event Action OnFinishGameRequest;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;

        OnFinishGameRequest?.Invoke();
    }
    #endregion
}
