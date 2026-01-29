using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    #region VARIABLES
    [Header ("Save state")]
    private bool hasSaved = false;

    [Header ("Animations")]
    Animator checkpointAnimator;

    [Header ("SFX")]
    [SerializeField] AudioClip checkpointSFX;
    #endregion

    #region EVENTS
    void Start()
    {
        checkpointAnimator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (hasSaved) return;

        AudioSource.PlayClipAtPoint(checkpointSFX, transform.position);
        SaveSystem.SaveGame();
        hasSaved = true;

        checkpointAnimator.SetTrigger("Checked");
    }
    #endregion
}