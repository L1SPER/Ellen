using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;//normalde 200
    [SerializeField] float runningSpeed = 10f;//Koşmanın 2 katı olmalı.
    [SerializeField] float jumpForce = 400f;


    //public bool isGrounded;
    float isGroundedRayLength = 1.5f;
    private bool facingRight = true;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private PlayerAnimationController playerAnimationController;
    private PlayerCombat playerCombat;
    private bool isWalking = false;
    
    public LayerMask platformLayerMask;

    private void Awake()    
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerCombat = GetComponent<PlayerCombat>();
    }
    void FixedUpdate()
    {
        //Movement();
        HandleMovement();
        IsGrounded();
    }
    private void Update()
    {
        Debug.Log("isWalking is " + isWalking);
        HandleJumping();
        FaceControl();
    }

    private void HandleJumping()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && IsGrounded())
        {
            isWalking = false;
            rigidbody2D.velocity = Vector2.up * jumpForce;
            playerAnimationController.PlayJumpAnim();
            FindObjectOfType<AudioManager>().Play("PlayerJump");
        }
    }

    private void HandleMovement()
    {
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (Input.GetKey(KeyCode.A))
        {
            isWalking = true;
            //Walking phase
            if (facingRight)
                FlipFace();
            facingRight = false;
            RBWalkingAndRunningPhase(-movementSpeed);
            playerAnimationController.PlayWalkAnim();
            if(isWalking)
                FindObjectOfType<AudioManager>().Play("PlayerWalk");
            if (Input.GetMouseButton(0))
            {
                //Attack phase
                playerAnimationController.PlayAttackAnim();
                FindObjectOfType<AudioManager>().Play("PlayerAttack");
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //Running phase
                RBWalkingAndRunningPhase(-runningSpeed);
                playerAnimationController.PlayRunAnim();
                if (Input.GetMouseButton(0))
                {
                    //Attack phase
                    playerAnimationController.PlayAttackAnim();
                    FindObjectOfType<AudioManager>().Play("PlayerAttack");
                }
            }

        }
        else if (Input.GetKey(KeyCode.D))
        {
            isWalking = true;
            //Walking phase
            if (!facingRight)
                FlipFace();
            facingRight = true;
            RBWalkingAndRunningPhase(+movementSpeed);
            playerAnimationController.PlayWalkAnim();
            
            if (isWalking )
                FindObjectOfType<AudioManager>().Play("PlayerWalk");

            if (Input.GetMouseButton(0))
            {
                //Attack phase
                playerAnimationController.PlayAttackAnim();
                FindObjectOfType<AudioManager>().Play("PlayerAttack");
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //Running phase
                RBWalkingAndRunningPhase(+runningSpeed);
                playerAnimationController.PlayRunAnim();
                if (Input.GetMouseButton(0))
                {
                    playerAnimationController.PlayAttackAnim();
                    FindObjectOfType<AudioManager>().Play("PlayerAttack");
                }
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            isWalking = false;
            //Crouch phase
            RBIdleAndCrouchPhase();
            playerAnimationController.PlayCrouchAnim();
        }
        else
        {
            isWalking = false;
            //Idle phase
            RBIdleAndCrouchPhase();
            playerAnimationController.PlayIdleAnim();
            if (Input.GetMouseButton(0))
            {
                playerAnimationController.PlayAttackAnim();
                FindObjectOfType<AudioManager>().Play("PlayerAttack");
            }
        }
    }

    private void RBWalkingAndRunningPhase(float speed)
    {
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }

    private void RBIdleAndCrouchPhase()
    {
        rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
    }
    private void FaceControl()
    {
        if (rigidbody2D.velocity.x < 0 && facingRight)
        {
            //FlipFace(true);
            FlipFace();
        }
        else if (rigidbody2D.velocity.x > 0 && !facingRight)
        {
            //FlipFace(false);
            FlipFace();

        }
    }

    //private void FlipFace(bool flip)
    //{
    //    facingRight = !facingRight;
    //    spriteRenderer.flipX = flip;
    //}
    public IEnumerator PlayAudio(string name)
    {
        FindObjectOfType<AudioManager>().Play(name);
        yield return new WaitForSeconds(1f);
    }

    private void FlipFace()
    {
        Vector3 flip = transform.localScale;
        flip.x *= -1;
        transform.localScale = flip;
        facingRight = !facingRight;
    }

    // private void Movement()
    // {

    //     if (Input.GetKey(KeyCode.A))
    //     {
    //         //Walking phase
    //         spriteRenderer.flipX = true;
    //         transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
    //         WalkAnim();
    //         if (Input.GetKey(KeyCode.LeftShift))
    //         {
    //             //Running phase
    //             transform.Translate(Vector3.left * Time.deltaTime * movementSpeed * 2f);
    //             RunAnim();
    //         }
    //     }
    //     else if (Input.GetKey(KeyCode.D))
    //     {
    //         //Walking phase
    //         spriteRenderer.flipX = false;
    //         transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
    //         WalkAnim();
    //         if (Input.GetKey(KeyCode.LeftShift))
    //         {
    //             //Running phase
    //             transform.Translate(Vector3.right * Time.deltaTime * movementSpeed * 2);
    //             RunAnim();
    //         }
    //     }
    //     else if (Input.GetKey(KeyCode.S))
    //     {
    //         //Crouch phase
    //         CrouchAnim();
    //     }
    //     else
    //     {
    //         //Idle phase
    //         IdleAnim();
    //     }
    //     if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
    //     {
    //         transform.Translate(Vector3.up * Time.deltaTime * movementSpeed * 3);
    //         JumpAnim();
    //     }
    // }

    private bool IsGrounded()
    {

        RaycastHit2D raycastHit2D = Physics2D.BoxCast(spriteRenderer.bounds.center, spriteRenderer.bounds.size, 0f, Vector2.down, isGroundedRayLength, platformLayerMask);
        // Color rayColor;
        // if(raycastHit2D.collider!=null)
        // {
        //     rayColor=Color.green;
        // }
        // else
        // {
        //     rayColor=Color.red;
        // }
        // Debug.DrawRay(spriteRenderer.bounds.center+new Vector3(spriteRenderer.bounds.extents.x,0),Vector2.down*(spriteRenderer.bounds.extents.y+isGroundedRayLength),rayColor);

        // //Debug.DrawRay(spriteRenderer.bounds.center+new Vector3(spriteRenderer.bounds.extents.x,0),Vector2.down*(spriteRenderer.bounds.extents.y+isGroundedRayLength),rayColor);

        // Debug.DrawRay(spriteRenderer.bounds.center+new Vector3(0,spriteRenderer.bounds.extents.y),Vector2.down*(spriteRenderer.bounds.extents.y+isGroundedRayLength),rayColor);
        // if(raycastHit2D.collider!=null)
        //     Debug.Log("We are colliding with "+raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }


}


