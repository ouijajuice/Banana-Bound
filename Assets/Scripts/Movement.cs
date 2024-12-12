using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    public float maxDistance;
    [SerializeField]
    private string inputNameHorizontal;
    [SerializeField]
    private string inputNameJump;
    [SerializeField]
    private string inputNameInteract;

    private float horizontal;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float jumpingPower;

    private bool isFacingRight = true;

    [SerializeField] 
    private Rigidbody2D rb;
    [SerializeField] 
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheckL;
    [SerializeField]
    private Transform wallCheckR;
    [SerializeField] 
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask wallLayer;
    [SerializeField]
    private LayerMask otherPlayerLayer;
    [SerializeField]
    private GameObject otherPlayerObject;

    [SerializeField]
    private float pullForce;
    private Rigidbody2D otherPlayerRb;

    private Animator animator;
    public bool jumping;
    public bool running;
    public bool grab;
    private void Start()
    {
        otherPlayerRb = otherPlayerObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw(inputNameHorizontal);

        if (Input.GetButtonDown(inputNameJump) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumping = true;
        }

        if (Input.GetButtonUp(inputNameJump) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        
        if (IsGrounded() == false && rb.velocity.y > 0f)
        {
            animator.SetBool("Jumping", true);
        }
        if (IsGrounded() == false && rb.velocity.y < 0f)
        {
            animator.SetBool("Falling", true);
        }
        if (IsGrounded())
        {
            animator.SetBool("Falling", false);
            animator.SetBool("Jumping", false);
        }

        //Debug.Log(horizontal);

        Flip();

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            running = true;
        }
        else
        {
            running = false;
        }
        animator.SetFloat("Horizontal", Mathf.Abs(horizontal));
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.AddForce(Vector2.right * horizontal * speed);
        }
        Vector2 toOtherPlayer = otherPlayerObject.transform.position - transform.position;
        if (toOtherPlayer.magnitude > maxDistance)
        {
            // Calculate the overshoot and apply a corrective force
            Vector2 pullForce = toOtherPlayer.normalized * (toOtherPlayer.magnitude - maxDistance);
            rb.AddForce(pullForce * speed, ForceMode2D.Force);
        }
        if (Input.GetButton(inputNameInteract) && (IsGrounded() || IsOnWall()))
        {
            PullOtherPlayer();
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        if (IsOnWall() == true)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.1f, 0);
            grab = true;
        }
        grab = false;
    }

    private bool IsGrounded()
    {
        
        if (Physics2D.OverlapBox(groundCheck.position, new Vector2(0.95f, 0.01f), 0, groundLayer))
        {
            jumping = false;
            return true;
        }
        else if (Physics2D.OverlapBox(groundCheck.position, new Vector2(0.95f, 0.01f), 0, otherPlayerLayer))
        {
            jumping = false;
            return true;
        }
        else 
        { 
            return false; 
        }
        
    }

    private bool IsOnWall()
    {
        //detect if a wall is within range of the wall detection transform
        if (isFacingRight)
        {
            if (Physics2D.OverlapCircle(wallCheckR.position, 0.05f, wallLayer) && (horizontal > 0))
            {
                animator.SetFloat("Horizontal", 0f);
                //animator.SetBool("animDashing", false);
                animator.SetBool("Jumping", false);
                animator.SetBool("Falling", false);
                animator.SetBool("OnWall", true);
                //rb.bodyType = RigidbodyType2D.Kinematic;
                grab = true;
                return true;
            }
            else
            {
                animator.SetBool("OnWall", false);
                //rb.bodyType = RigidbodyType2D.Dynamic;
                grab = false;
                return false;
            }
            
        }
        if (!isFacingRight)
        {
            if (Physics2D.OverlapCircle(wallCheckR.position, 0.05f, wallLayer) && (horizontal < 0))
            {
                animator.SetFloat("Horizontal", 0f);
                //animator.SetBool("animDashing", false);
                animator.SetBool("Jumping", false);
                animator.SetBool("Falling", false);
                animator.SetBool("OnWall", true);
                //rb.bodyType = RigidbodyType2D.Kinematic;
                grab = true;
                return true;
            }
            else
            {
                animator.SetBool("OnWall", false);
                //rb.bodyType = RigidbodyType2D.Dynamic;
                grab = false;
                return false;
            }
        }
        return false;
    }

    private bool IsAtMaxDistance()
    {
        if (Physics2D.OverlapCircle(this.transform.position, maxDistance, otherPlayerLayer))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void PullOtherPlayer()
    {
        // Calculate the vector from the other player to the current player
        Vector2 pullDirection = (transform.position - otherPlayerObject.transform.position).normalized;

        // Apply a force to the other player in the direction of the current player
        otherPlayerRb.AddForce(pullDirection * pullForce);
    }
}
