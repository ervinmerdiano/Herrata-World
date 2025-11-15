using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health Settings")]
    public int maxHealth = 100;
    public bool IsDead { get; private set; } = false;
    private int currentHealth;

    private Vector2 lastDamageSource;

    private Animator animator;

    [Header("Audio")]
    public AudioSource hurtSound;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }


    public void TakeDamage(int amount, Vector2 attackerPosition)
    {
        lastDamageSource = attackerPosition;
        TakeDamage(amount);


        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {

            Vector2 recoilDir = ((Vector2)transform.position - lastDamageSource).normalized;

            float recoilForce = 12f;
            rb.velocity = Vector2.zero;
            rb.AddForce(recoilDir * recoilForce, ForceMode2D.Impulse);
        }
    }


    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        GetComponent<PlayerMovement>().canMove = false;

        Debug.Log("Player is hurt! Current HP: " + currentHealth);


        if (hurtSound != null)
            hurtSound.Play();


        if (animator != null)
        {
            animator.SetTrigger("Hurt");
            GetComponent<PlayerMovement>().canMove = true;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        IsDead = true;

        Debug.Log("Player died!");
        if (animator != null)
        {
            animator.SetBool("isDead", true);
            animator.SetFloat("speed", 0f);
            animator.SetBool("isRunning", false);
        }

        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = false;

        AudioSource[] allAudio = GetComponents<AudioSource>();
        foreach (AudioSource s in allAudio)
        {
            if (s.isPlaying)
                s.Stop();
        }

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log("Healed! Current HP: " + currentHealth);
    }
}
