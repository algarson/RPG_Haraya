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

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with tag: " + collision.tag);
        if (collision.CompareTag("OrcAttack") && collision.gameObject != gameObject) // Ensure the collider is not the player's // Ensure this is the tag you've set on your orc attack collider
        {
            Debug.Log("Collided with Orc Attack");
            // Get the Orc_atk script component from the collider
            Orc_atk orcAttack = collision.GetComponent<Orc_atk>();
            if (orcAttack != null)
            {
                Debug.Log("Taking damage: " + orcAttack.attackDamage);
                TakeDamage(orcAttack.attackDamage); // Use the attackDamage from the Orc_atk script
            }
        }
    }



    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }


}
