using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public float movSpeed;
    float speedX, speedY;
    Rigidbody2D rb;
    private Animator anim;
    public int maxHealth = 20;
    public int currentHealth;
    public HealthBar healthBar;

    private float accumulatedHealth = 0f;
    public float hpRegenRate = 1f; // Health points regenerated per second
    public float regenCooldown = 5f; // Time in seconds before HP regen starts after taking damage
    private float regenTimer = 0f;

    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 characterScale = transform.localScale;
        if(Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = (float)-0.5852;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = (float)0.5852;
        }
        transform.localScale = characterScale;


        if (speedX == 0 && speedY == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            anim.SetTrigger( name: "isSwordMode");
        };
        if (Input.GetKey(KeyCode.Alpha1))
        {
            anim.SetTrigger(name: "isNormalMode");
        }


        speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movSpeed;
        rb.velocity = new Vector2(speedX, speedY);

        if (regenTimer > 0)
        {
            regenTimer -= Time.deltaTime;
        }
        else
        {
            // Regenerate HP if below max and not on cooldown
            if (currentHealth < maxHealth)
            {
                RegenerateHealth();
            }
        }

    }
    void RegenerateHealth()
    {
        // Accumulate health in a float.
        accumulatedHealth += hpRegenRate * Time.deltaTime;

        // Only convert to int and add to currentHealth when at least 1 health point has been accumulated.
        while (accumulatedHealth >= 1f)
        {
            currentHealth += 1;
            accumulatedHealth -= 1f;
        }

        // Ensure current health does not exceed max health.
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        // Update the health bar.
        healthBar.SetHealth(currentHealth);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OrcAttack") && collision.gameObject != gameObject) // Ensure the collider is not the player's // Ensure this is the tag you've set on your orc attack collider
        {
            // Get the Orc_atk script component from the collider
            Orc_atk orcAttack = collision.GetComponent<Orc_atk>();
            if (orcAttack != null)
            {
                TakeDamage(orcAttack.attackDamage); // Use the attackDamage from the Orc_atk script
            }
        }
        if (collision.CompareTag("Enemy"))
        {
            int attackDamage = 3;

            EnemyAI enemyAI = collision.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.TakeDamage(attackDamage);
            }
        }
        if (collision.CompareTag("ManaAtk") && collision.gameObject != gameObject) // Ensure the collider is not the player's // Ensure this is the tag you've set on your orc attack collider
        {
            // Get the ManaAtk script component from the collider
            mana_atk ManaAtk = collision.GetComponent<mana_atk>();
            if (ManaAtk != null)
            {
                TakeDamage(ManaAtk.attackDamage); // Use the attackDamage from the ManaAtk script
            }
        }
    }



    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        regenTimer = regenCooldown; // Reset the regeneration timer when taking damage

        if (currentHealth <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }


}
