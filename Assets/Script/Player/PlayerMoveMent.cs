using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
    [SerializeField] private float speed = 11f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private AudioClip jumpSound;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;

    private float moveInput;
    private float wallJumpCoolDown;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    Animator animator;
    bool isGround;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        if (moveInput < 0f) 
            transform.localScale = new Vector3(-1, 1, 1);
        else if (moveInput > 0f) 
            transform.localScale = new Vector3(1, 1, 1);
        
        animator.SetBool("run", moveInput !=0 );
        animator.SetBool("grounded", IsGround());

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump();   
        }

        //Adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        if (onWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 4;
            body.velocity = new Vector2(moveInput * speed, body.velocity.y);
            if (IsGround())
            {
                coyoteCounter = coyoteTime;
                jumpCounter = extraJumps;
            }
            else
                coyoteCounter -= Time.deltaTime; 
        }
    }

    private void PlayerJump()
    {
        if (coyoteCounter <= 0 && !onWall()) return;
        SoundManager.instance.PlaySound(jumpSound);
        if (onWall())
            WallJump();
        else
        {
            if (IsGround())
                body.velocity = new Vector2(body.velocity.x, jumpForce);
            else
            {
                if (coyoteCounter > 0)
                    body.velocity = new Vector2(body.velocity.x, jumpForce);
                else
                {
                    if (jumpCounter > 0)
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpForce);
                        jumpCounter--;
                    }
                }
            }
            //reset coyotetime to 0
            coyoteCounter = 0;
        }
    }
    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCoolDown = 0;
    }

    private bool IsGround()
    {
        return isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0 , new Vector2(transform.localScale.x, 0),0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack()
    {
        return moveInput == 0 && isGround && !onWall();

    }

}
