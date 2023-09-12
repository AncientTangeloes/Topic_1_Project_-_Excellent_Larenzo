using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private float dirX = 0f;
    private float jumpCount = 0f;
    
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float jumpHeight = 20f;
    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState { idle, walking, jumping, falling }
    
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Hello Lorenzo.");

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        Debug.Log("Components initialized.");
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Walk");
        rb.velocity = new Vector2(dirX * walkSpeed, rb.velocity.y);
        
        if (Input.GetButtonDown("Jump") && jumpCount < 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            jumpCount++;
            //Debug.Log("Jump, Larenzo, jump!");
        }

        if (IsGrounded())
        {
            jumpCount = 0f;
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        
        if (dirX != 0f)
        {
            state = MovementState.walking;

            if (dirX > 0f)
            {
                sprite.flipX = false;
            }
            else if (dirX < 0f)
            {
                sprite.flipX= true;
            }
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .001f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.001f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int) state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
