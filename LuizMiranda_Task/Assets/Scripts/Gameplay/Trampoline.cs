using Unity.VisualScripting;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    #region VARIABLES
    [Header ("Bounce Settings")]
    [SerializeField] float baseBounceForce = 6f;
    [SerializeField] float bounceMultiplier = 1.2f;
    [SerializeField] float maxBounceForce = 12f;

    [Header ("Animations")]
    Animator trampolineAnimator;
    #endregion

    #region EVENTS
    void Start()
    {
        trampolineAnimator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;

        TriggerTrampoline(collision);  //Throw the player highest than normal jump based on the falling distance   
    }
    #endregion

    #region METHODS
    void TriggerTrampoline(Collider2D collision)
    {
        Rigidbody2D rigidbody = collision.attachedRigidbody;
        if(rigidbody == null) return;

        if(Mathf.Abs(rigidbody.linearVelocity.y) > Mathf.Epsilon)
        {
            float fallSpeed = Mathf.Abs(rigidbody.linearVelocity.y);
            float bounceForce = baseBounceForce + (fallSpeed * bounceMultiplier);

            bounceForce = Mathf.Clamp(bounceForce, baseBounceForce, maxBounceForce);
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, bounceForce);
            trampolineAnimator.SetTrigger("Jump");
        }        
    }
    #endregion
}
