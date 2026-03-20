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
    public float prebufferTime = .2f;
    private float currentPrebufferTime;
    public float coyoteJumpTime = .2f;
    private float currentCoyoteJumpTime;
    private bool jumping = false;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale;               
    }
    private void Update() 
    {
        float xInput = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(xInput * speedPlayer, rb.linearVelocity.y);

        if(currentPrebufferTime > 0)
        {
            currentPrebufferTime -=1*Time.deltaTime;
        }

        if(Input.GetButtonDown("Jump"))
        {
            currentPrebufferTime = prebufferTime;
            if(!jumping && rb.linearVelocityY < 0)
            {
                currentCoyoteJumpTime = coyoteJumpTime;
            }
        }

        if(currentCoyoteJumpTime > 0)
        {
            currentCoyoteJumpTime -= 1 * Time.deltaTime;
        }
        if(jumping && IsGrounded() && rb.linearVelocityY == 0)
        {
            jumping = false;
        }     

        if(currentPrebufferTime >0 && IsGrounded() 
        && !jumping || ( currentCoyoteJumpTime >0&&!jumping))
        {
            jumping = true;
            rb.linearVelocityY = 0;
            currentCoyoteJumpTime = 0;
            currentPrebufferTime = 0;
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
    public bool IsGrounded()
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
