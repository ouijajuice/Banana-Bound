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
    private void Start()
    {
        otherPlayerRb = otherPlayerObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw(inputNameHorizontal);

        if (Input.GetButtonDown(inputNameJump) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp(inputNameJump) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if(IsOnWall() == true)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.1f, 0);
        }

        //Debug.Log(horizontal);

        Flip();
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
        if (Input.GetButton(inputNameInteract) && IsGrounded())
        {
            PullOtherPlayer();
        }
    }

    private bool IsGrounded()
    {
        if(Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer))
        {
            return true;
        }
        else if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, otherPlayerLayer))
        {
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
        if (Physics2D.OverlapCircle(wallCheckR.position, 0.05f, wallLayer))
        {
            //animator.SetFloat("Horizontal", 0f);
            //animator.SetBool("animDashing", false);
            //animator.SetBool("animJumpUp", false);
            //animator.SetBool("animFalling", false);
            //animator.SetBool("animOnWall", true);
            //rb.bodyType = RigidbodyType2D.Kinematic;
            return true;
        }
        else
        {
            //animator.SetBool("animOnWall", false);
            //rb.bodyType = RigidbodyType2D.Dynamic;
            return false;
        }
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
