using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float PlayerSpeed = 10;
    [SerializeField] private float PlayerJumpPower = 12;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;

    private float jumpWallCooldown;
    private float HorizontalInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");

        if(HorizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if(HorizontalInput < -0.01f)
        {
            transform.localScale = new Vector3 (-1, 1, 1);
        }

        

        animator.SetBool("run", HorizontalInput != 0);
        animator.SetBool("ground", IsGrounded());

        if (jumpWallCooldown > 0.2f)
        {

            body.velocity = new Vector2(HorizontalInput*PlayerSpeed, body.velocity.y);

            if (onTheWall() && !IsGrounded())
            {
                body.velocity = Vector2.zero;
                body.gravityScale = 0;
                

            }
            else
            {
                body.gravityScale = 3;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }

        }
        else
        {
            jumpWallCooldown += Time.deltaTime;
        }

    }

    private void Jump()
    {
        if (IsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, PlayerJumpPower);
            animator.SetTrigger("jump");
        }
        else if(onTheWall() && !IsGrounded())
        {
            
            if(HorizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, -2);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                

            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            jumpWallCooldown = 0;
            
        }
        


    }

    private bool IsGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.01f, groundLayer);
        return rayCastHit.collider != null;
    }

    private bool onTheWall()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.01f, wallLayer);
        return rayCastHit.collider != null;
    }

    public bool canAttack()
    {
        return (HorizontalInput == 0) && IsGrounded() && !onTheWall();
    }
}
