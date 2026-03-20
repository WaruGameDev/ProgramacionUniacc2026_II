using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    
    public Animator anim;
    public PlayerController playerController;
    public Transform visual;
    void Start()
    {        
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Ground", playerController.IsGrounded());
        anim.SetFloat("Horizontal", Mathf.Abs(playerController.rb.linearVelocityX));
        anim.SetFloat("Vertical", playerController.rb.linearVelocityY);
        if(playerController.rb.linearVelocityX < 0)
        {
            visual.localScale = new Vector3(-1,1,1);
        }
        else if(playerController.rb.linearVelocityX > 0)
        {
            visual.localScale = new Vector3(1,1,1);
        }
    }
}
