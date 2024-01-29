using UnityEngine;
using System.Collections;
public class OrcMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public Transform target; // Assign the main character's transform here in the inspector
    public float stoppingDistance = 1.0f; // The distance at which the Orc should stop moving towards the player
    public float attackRange = 1.0f; // The range within which the Orc can attack
    public float attackCooldown = 2.0f; // Time between attacks

    private float lastAttackTime = 0; // When the last attack was performed

    public Collider2D orcAttackCollider;
    public int orcAttackDamage = 5;

    private Rigidbody2D rb;
    private Animator animator;

    public int maxHealth = 20;
    public int currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check the distance to the target
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        // If the target is within attack range and cooldown is over, attack
        if (distanceToTarget <= attackRange && Time.time > lastAttackTime + attackCooldown)
        {
            Attack();
        }
        // If we're close enough to the target, stop moving by setting isRunning to false
        if (distanceToTarget <= stoppingDistance)
        {
            animator.SetBool("isRunning", false);
            return; // Exit early if we're not supposed to move
        }
        else
        {
            animator.SetBool("isRunning", true);
        }

        // Determine the direction to the target
        Vector2 direction = (target.position - transform.position).normalized;

        // Move the Orc
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

        // Optional: If you want the Orc to face the target, you would adjust the sprite's flip here
        if (direction.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false; // Assuming the Orc faces right by default
        }
        else if (direction.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }



    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Play death animation, disable the orc, etc.
        animator.SetTrigger("orc_die");
        // Disable the orc's movement and attacks, for example:
        this.enabled = false;
        rb.velocity = Vector2.zero;
        StartCoroutine(DisappearAfterDelay());
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true; // Prevents the Rigidbody from being affected by physics
        StartCoroutine(DisappearAfterDelay());
        // Optionally destroy the orc game object or disable it
        // Destroy(gameObject);
        // Or simply disable it
        // gameObject.SetActive(false);
    }


    IEnumerator DisappearAfterDelay()
    {
        // Wait for the length of the animation before continuing
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false); // This will disable the orc GameObject
                                     // Or, if you want to completely remove the orc from the game:
                                     // Destroy(gameObject);
    }


    void Attack()
    {
        // Choose an attack based on some condition, random or otherwise
        bool useAttack1 = Random.Range(0, 2) == 0; // 50% chance for either attack
        EnableOrcAttackCollider();
        DisableOrcAttackCollider();
        if (useAttack1)
        {
            animator.SetTrigger("isOrc_atk1");
        }
        else
        {
            animator.SetTrigger("isOrc_atk2");
        }

        lastAttackTime = Time.time; // Reset the last attack time to the current time
    }
    void EnableOrcAttackCollider()
    {
        if (orcAttackCollider != null)
        {
            orcAttackCollider.enabled = true;
        }
    }

    void DisableOrcAttackCollider()
    {
        if (orcAttackCollider != null)
        {
            orcAttackCollider.enabled = false;
        }
    }

    // If you want more precise physics-based movement, you can use FixedUpdate
    // However, for simple AI movement, Update is usually sufficient and easier to sync with animations
    public void EndAttack()
    {
        animator.ResetTrigger("isOrc_atk1");
        animator.ResetTrigger("isOrc_atk2");
        // Add any other logic you want to happen at the end of an attack
    }
}
