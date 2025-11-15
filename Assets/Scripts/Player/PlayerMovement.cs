using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 4f;
    public float runSpeed = 7f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput;
    private bool isRunning;
    public bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

         if (!canMove)
        {
            rb.velocity = Vector2.zero;
            anim.SetFloat("speed", 0);
            return;
        }
        
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");


        bool isMoving = moveInput.magnitude > 0;

        isRunning = isMoving && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));

        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        rb.velocity = moveInput.normalized * currentSpeed;

        anim.SetFloat("speed", rb.velocity.magnitude);
        anim.SetBool("isRunning", isRunning);

        if (moveInput.x != 0)
            spriteRenderer.flipX = moveInput.x < 0;
    }
    
    public bool IsMoving()
    {
        return moveInput.magnitude > 0;
    }

    public bool IsRunning()
    {
        return isRunning;
    }

}