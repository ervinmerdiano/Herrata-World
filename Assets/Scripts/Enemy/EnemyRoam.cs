using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyRoam : MonoBehaviour
{
    [Header("Roam Settings")]
    public float moveSpeed = 2f;
    public float roamTime = 2f;
    public float idleTime = 3f;

    [Header("Player Detection")]
    public float detectRange = 4f; 
    public float stopDistance = 0.5f;
    public float loseSightDelay = 1f;

    [Header("Attack Settings")]
    public float attackRange = 0.6f;
    public float attackCooldown = 1.5f;
    public int attackDamage = 10;     

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Transform player;
    private bool isChasing = false;
    private float loseSightTimer = 0f;
    private bool isAttacking = false;
    private float nextAttackTime = 0f;
    private PlayerHealth playerHealth;

    public AudioSource attackSound;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        
        if (player != null){
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        StartCoroutine(Roam());
    }

    private void Update()
    {
        if (player == null) 
            return;

        if (playerHealth != null && playerHealth.IsDead)
        {
            if (isChasing || isAttacking)
            {
                isChasing = false;
                isAttacking = false;
                rb.velocity = Vector2.zero;
                animator.SetBool("isWalking", false);
            }

            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectRange)
        {
            isChasing = true;
            loseSightTimer = 0f;
        }

        else if (isChasing)
        {
            loseSightTimer += Time.deltaTime;
            if (loseSightTimer >= loseSightDelay)
            {
                isChasing = false;
                loseSightTimer = 0f;
                rb.velocity = Vector2.zero;
                animator.SetBool("isWalking", false);
            }
        }


        if (isChasing)
        {
            ChasePlayer(distance);
        }

        if (isChasing)
        {
            spriteRenderer.flipX = player.position.x < transform.position.x;
        }
        else
        {
            if (rb.velocity.x != 0)
                spriteRenderer.flipX = rb.velocity.x < 0;
        }
    }

    private void ChasePlayer(float distance)
    {
        if (distance <= attackRange)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isWalking", false);

            if (Time.time >= nextAttackTime && !isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
        else if (distance > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
            animator.SetBool("isWalking", true);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isWalking", false);
        }
    }


    private System.Collections.IEnumerator Roam()
    {
        while (true)
        {
            if (isChasing || isAttacking)
            {
                yield return null;
                continue;
            }

            Vector2 randomDir = Random.insideUnitCircle.normalized;
            rb.velocity = randomDir * moveSpeed;
            animator.SetBool("isWalking", true);

            if (randomDir.x != 0){
                spriteRenderer.flipX = randomDir.x < 0;
            }

            float timer = 0f;
            while (timer < roamTime)
            {
                if (isChasing || isAttacking)
                    break;

                if (rb.velocity.magnitude < 0.1f)
                {
                    rb.velocity = Vector2.zero;
                    animator.SetBool("isWalking", false);
                    yield return new WaitForSeconds(idleTime);
                    break;
                }

                timer += Time.deltaTime;
                yield return null;
            }

            rb.velocity = Vector2.zero;
            animator.SetBool("isWalking", false);

            yield return new WaitForSeconds(idleTime);
        }
    }


    public void PlayAttackSound()
    {
        if (attackSound != null)
            attackSound.Play();
    }

    private System.Collections.IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        rb.velocity = Vector2.zero;
        

        yield return new WaitForSeconds(0.4f);

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(attackDamage, transform.position);
            }
        }

        nextAttackTime = Time.time + attackCooldown;
        yield return new WaitForSeconds(attackCooldown * 0.5f);

        isAttacking = false;
    }
}
