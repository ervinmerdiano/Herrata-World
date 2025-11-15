using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movement;
    private bool isAttacking = false;

    public AudioSource attackSound;
    // private SpriteRenderer sr;

    public void PlayAttackSound()
    {
        if (attackSound != null)
            attackSound.Play();
    }

    // public void DamageEnemy(Collider2D col)
    // {
    //     EnemyRoam enemy = col.GetComponent<EnemyRoam>();
    //     if (enemy != null)
    //     {
    //         enemy.TakeHit();
    //     }
    // }


    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        // sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        bool isMoving = Mathf.Abs(moveInput) > 0.1f;

        if (Input.GetMouseButtonDown(0) && !isAttacking && !isMoving)
        {
            Attack();
        }
    }

    private void Attack()
    {
        isAttacking = true;
        movement.canMove = false;
        animator.SetTrigger("attackTrigger");
    }

    private void EndAttack()
    {
        isAttacking = false;
        movement.canMove = true;

        // Vector2 dir = sr.flipX ? Vector2.left : Vector2.right;

        // RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1f);

        // if (hit.collider != null)
        //     {
        //         EnemyRoam enemy = hit.collider.GetComponent<EnemyRoam>();
        //         if (enemy != null)
        //             {
        //                 enemy.TakeHit();
        //             }
        //     }
    }
}
