using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Vector2 playerVeloicity;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myCollider;
    // Start is called before the first frame update

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
    }

    void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue input)
    {
       
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) )
        {
            myRigidBody.velocity += Vector2.up * jumpSpeed;
            
        }
    }

    private void Run()
    {
        playerVeloicity = new Vector2(moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVeloicity;
        myAnimator.SetBool("Running", Mathf.Abs(myRigidBody.velocity.x)>0);
    }

    void ClimbLadder()
    {
        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = 9.8f;
            return;

        }
        myRigidBody.gravityScale = 0;
        playerVeloicity = new Vector2(myRigidBody.velocity.x, moveInput.y * climbSpeed);
        myRigidBody.velocity = playerVeloicity;
        myAnimator.SetBool("Climbing", Mathf.Abs(myRigidBody.velocity.y) > 0);
    }

    void FlipSprite()
    {
        if (Mathf.Abs(myRigidBody.velocity.x) > 0)
        {
            float direction = Mathf.Sign(myRigidBody.velocity.x);
            transform.localScale = new Vector2(direction, 1f);
        }
    }
    // Update is called once per frame

    void Update()
    {
        Run();
        ClimbLadder();
        FlipSprite();
    }
}
