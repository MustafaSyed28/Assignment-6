/* Author: Ahmed Mustafa
 * Date 9/22/2025
 * Description: controls platformer player
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayerController : MonoBehaviour
{

    //player movement speed
    public float moveSpeed = 5f;

    // force applied for jumping
    public float jumpForce = 10f;

    public LayerMask groundLayer;

    public Transform groundCheck;

    public float groundCheckRadius = 0.2f;

    // refrence to Rigid body2D
    private Rigidbody2D rb;

    private bool isGrounded;

    // a variable holds horizontal input
    private float horizontalInput;

    // Set these referance in the inspector
    public AudioClip jumpSound;

    // An audio to play sound effects
    private AudioSource playerAudio;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // set refrence
        playerAudio = GetComponent<AudioSource>();

        // get values for horizontal movement
        rb = GetComponent<Rigidbody2D>();

        // ensure the ground check variable is assigned
        if(groundCheck == null) 
        {
            Debug.LogError("groundCheck not assigned to the player controller!!");
        }


    }

    // Update is called once per frame
    void Update()
    {
        // get
        horizontalInput = Input.GetAxis("Horizontal");

        // check for jump input
        if(Input.GetButtonDown("Jump") && isGrounded) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // play jump soundEffect
            playerAudio.PlayOneShot(jumpSound, 1.0f);

            
        }

    }

    void FixedUpdate()

    {
        if (!PlayerHealth.hitRecently)
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }

        animator.SetFloat("xVelocityAbs", Mathf.Abs(rb.velocity.x));

        animator.SetFloat("yVelocity", rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        animator.SetBool("onGround", isGrounded);

        // optionally we can add animations here later

        // ensure the player is facing the direction of movement

        if (horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }


    }

}
