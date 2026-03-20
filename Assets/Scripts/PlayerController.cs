using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedPlayer =5;
    public float forceJump = 5;

    public Rigidbody2D rb;
    public Transform groundCheck;
    public float groundCheckRadius = .2f;
    public float lowMultiplier = 2;
    public float fallMultiplier =2.5f;
    private float originalGravityScale;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale;               
    }
    private void Update() 
    {
        float xInput = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(xInput * speedPlayer, rb.linearVelocity.y);

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
        }    

        if(rb.linearVelocityY >0 )
        {
            rb.gravityScale = lowMultiplier;
            if(!Input.GetButton("Jump"))
            {
                rb.linearVelocityY *= .95f;
            }
        }    
        else if(rb.linearVelocityY <0 )
        {
            rb.gravityScale = fallMultiplier;
        }
        else
        {
            rb.gravityScale = originalGravityScale;
        }
    }
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, 
        LayerMask.GetMask("Ground"));
    }
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        
    }

}
