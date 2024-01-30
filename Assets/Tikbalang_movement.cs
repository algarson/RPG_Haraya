using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 10f;
    public float slamAttackRange = 2f;
    public float chargeSpeed = 5f;
    public float chargeDuration = 3f;
    public float chargeCooldown = 5f;
    public float slamCooldown = 3f;

    private Animator animator;
    private Transform playerTransform;
    private bool isCharging;
    private bool shouldSlam;
    
    private float chargeTimer, chargeCooldownTimer, slamCooldownTimer;

    public int maxHealth = 30;
    public int currentHealth;
    public HealthBar healthBar;

    private Collider2D chargeAttackCollider;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        if (healthBar != null) // Make sure you have a null check for the health bar
        {
            healthBar.SetMaxHealth(maxHealth);
        }
        // Initialize your collider here
        chargeAttackCollider = GetComponent<Collider2D>(); // Make sure this matches the actual component
    }

    void Update()
    {
        UpdateCooldownTimers();

        if (isCharging)
        {
            ChargeTowardsPlayer();
        }
        else
        {
            DetectAndAttackPlayer();
        }
    }

    private void UpdateCooldownTimers()
    {
        if (chargeCooldownTimer > 0) chargeCooldownTimer -= Time.deltaTime;
        if (slamCooldownTimer > 0) slamCooldownTimer -= Time.deltaTime;
    }

    private void DetectAndAttackPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectionRange && chargeCooldownTimer <= 0)
        {
            PerformChargeAttack();
            animator.SetBool("Tik_charge", true);
        }
        else
        {
            animator.SetBool("Tik_charge", false);
        }

        if (distanceToPlayer <= slamAttackRange && slamCooldownTimer <= 0)
        {
            PerformSlamAttack();
            animator.SetBool("Tik_slam", true);
        }
        else
        {
            animator.SetBool("Tik_slam", false);
        }
    }

    private void PerformChargeAttack()
    {
        if (chargeCooldownTimer <= 0)
        {
            animator.SetTrigger("Tik_charge");
            isCharging = true;
            chargeTimer = chargeDuration;
            chargeCooldownTimer = chargeCooldown;
            LookAtPlayer();
        }
    }

    private void PerformSlamAttack()
    {
        if (slamCooldownTimer <= 0)
        {
            if (isCharging)
            {
                StopCharge();
            }
            animator.SetTrigger("Tik_slam");
            slamCooldownTimer = slamCooldown;
        }
    }

    private void StopCharge()
    {
        isCharging = false;
        chargeTimer = 0; // Reset the charge timer
        animator.ResetTrigger("Tik_charge");
        animator.SetBool("Tik_idle", true); // Ensure the enemy goes back to idle
    }

    private void ChargeTowardsPlayer()
    {
        if (chargeTimer > 0)
        {
            chargeAttackCollider.enabled = true; // Enable the collider to deal damage during charge
            transform.position += transform.forward * chargeSpeed * Time.deltaTime;
            chargeTimer -= Time.deltaTime;
        }
        else
        {
            chargeAttackCollider.enabled = false; // Disable the collider once the charge is finished
            StopCharge();
        }
    }

    private void LookAtPlayer()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.y = 0;
        Quaternion rotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
    }
    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject.name + " took damage: " + damage); // Add this line to confirm the method is called
        currentHealth -= damage;

        // Update the health bar if it exists
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        // Play death animation
        animator.SetTrigger("TIk_dead");

        // Disable the enemy component
        // This stops the enemy from moving and attacking
        //Destroy(gameObject);
    }
}
