using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

public class PlayerActions : MonoBehaviour
{
    #region VARIABLES
    [Header ("Movement & Jump")]
    [SerializeField] float speed = 6f;
    [SerializeField] float jumpHigh = 6f;
    public Rigidbody2D playerRigidbody;
    Vector2 moveInput;
    Vector3 originalScale;
    CapsuleCollider2D playerCollider;
    BoxCollider2D playerFeetCollider;
    bool jumpInput = false;

    [Header ("Animation")]
    Animator playerAnimator;
    enum PlayerState { Idle = 0, Running = 1, Jumping = 2, Falling = 3};

    [Header ("SFX")]
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip eatFruitSFX;
    #endregion

    #region EVENTS
    void Start() 
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerAnimator = GetComponent<Animator>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        MovePlayer(); //Move player around the level
        FlipSprite(); //Flip Sprite to turn the player into the direction that he are moving
        UpdateAnimState(); //Check which animation should run and trigger it
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        jumpInput = value.isPressed;
        Jump();
    }

    void OnNext()
    {
        Inventory.Instance.NextItem();
    }

    void OnInteract(InputValue value)
    {
        if(!value.isPressed) return;

        AudioSource.PlayClipAtPoint(eatFruitSFX, transform.position);
        Inventory.Instance.UseSelectedItem();
    }
    #endregion

    #region METHODS
    void MovePlayer()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * speed, playerRigidbody.linearVelocity.y);
        playerRigidbody.linearVelocity = playerVelocity;
    }

    void FlipSprite()
    {
        bool isPlayerMoving = Mathf.Abs(playerRigidbody.linearVelocity.x) > Mathf.Epsilon;

        if(isPlayerMoving)
        {
            transform.localScale = new Vector3(
                Mathf.Sign(playerRigidbody.linearVelocity.x) * Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }
    }

    void Jump()
    {
        if(playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && jumpInput)
        {
            AudioSource.PlayClipAtPoint(jumpSFX, transform.position);
            playerRigidbody.linearVelocity = new Vector2(playerRigidbody.linearVelocity.x, jumpHigh);
            jumpInput = false;
        }
    }

    void UpdateAnimState()
    {
        if(!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            //In the air -> Jumping or falling
            if(playerRigidbody.linearVelocity.y > 0.1f)
                SetAnimState(PlayerState.Jumping);
            else
                SetAnimState(PlayerState.Falling);
        }
        else
        {
            //On ground -> Idle or running
            bool isPlayerMoving = Mathf.Abs(playerRigidbody.linearVelocity.x) > Mathf.Epsilon;
            if(isPlayerMoving)
                SetAnimState(PlayerState.Running);
            else
                SetAnimState(PlayerState.Idle);
        }
    }

    void SetAnimState(PlayerState newState)
    {
        if(playerAnimator.GetInteger("State") != (int)newState)
        {
            playerAnimator.SetInteger("State", (int)newState);
        }
    }
    #endregion

}
