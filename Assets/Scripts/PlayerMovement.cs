using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Vector2 playerVeloicity;
    [SerializeField] float runSpeed = 5f;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

    }
   void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
        Debug.Log(moveInput);
    }
    void OnJump(InputValue input)
    {
        RaycastHit2D ray = Physics2D.Raycast(myRigidBody.transform.position + (Vector3.down / 2), Vector2.down, 0.1f);
        print(ray.distance);
        if (ray.distance == 0)
        {
            myRigidBody.velocity += Vector2.up * 5;
            
        }
    }
    private void Run()
    {
        playerVeloicity = new Vector2(moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVeloicity;
        myAnimator.SetBool("Running", Mathf.Abs(myRigidBody.velocity.x)>0);
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
        FlipSprite();
    }
}
