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
    BoxCollider2D playerCollider;
    bool jumpInput = false;

    [Header ("Animation")]
    Animator playerAnimator;
    enum PlayerState { Idle = 0, Running = 1, Jumping = 2, Falling = 3};
    #endregion

    #region EVENTS
    void Start() 
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerAnimator = GetComponent<Animator>();
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
        if(playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && jumpInput)
        {
            playerRigidbody.linearVelocity = new Vector2(playerRigidbody.linearVelocity.x, jumpHigh);
            jumpInput = false;
        }
    }

    void UpdateAnimState()
    {
        if(!playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
