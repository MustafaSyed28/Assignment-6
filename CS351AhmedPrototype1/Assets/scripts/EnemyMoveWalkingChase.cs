using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class EnemyMoveWalkingChase : MonoBehaviour
{
    public float chaseRange = 4f;

    public float enemyMovementSpeed = 1.5f;

    private Transform playerTransform;

    private Rigidbody2D rb;

    private Animator anim;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 playerDirection = playerTransform.position - transform.position;

        float distanceToPlayer = playerDirection.magnitude;

        if (distanceToPlayer <= chaseRange)
        {
            playerDirection.Normalize();
            playerDirection.y = 0f;

            FacePlayer(playerDirection);

            if (IsGroundAhead())
            {
                MoveTowardsPlayer(playerDirection);
            }
            else
            {
                StopMoving();
            }

        }
        else
        {
            //stop moving if there is no ground ahead
            StopMoving();
        }

    }

    bool IsGroundAhead()
    {
        float groundCheckDistance = 2.0f;
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        Vector2 enemyFacingDirection = transform.rotation.y == 0 ? Vector2.left : Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down + enemyFacingDirection, groundCheckDistance, groundLayer);

        return hit.collider != null;

    }


    private void FacePlayer(Vector2 playerDirection)
    {
        if (playerDirection.x < 0)
        {
            sr.flipX = false;
        }
        else if (playerDirection.x > 0)
        {
            sr.flipX = true;
        }
    }


    private void MoveTowardsPlayer(Vector2 playerDirection)
    {
        rb.velocity = new Vector2(playerDirection.x * enemyMovementSpeed, rb.velocity.y);

        anim.SetBool("isMoving", true);
    }



    private void StopMoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);

        anim.SetBool("isMoving", false);
    }


}
