using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    private bool isCharging = false;
    public Collider2D attackCollider; // Assign this in the Inspector

    // Damage values for each attack
    public int normalAttackDamage = 3;
    public int attack2Damage = 5;
    public int attack3Damage = 7;
    public int chargedAttackDamage = 10;

    private int currentAttackDamage; // This will hold the damage of the current attack

    public int CurrentAttackDamage { get; internal set; }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click attack
        {
            currentAttackDamage = normalAttackDamage; // Set damage for this attack
            animator.SetTrigger("isNormal_Attack1");
        }
        if (Input.GetKeyDown(KeyCode.E)) // Attack 2
        {
            currentAttackDamage = attack2Damage; // Set damage for this attack
            animator.SetTrigger("isNormal_Attack2");
        }
        if (Input.GetKeyDown(KeyCode.Q)) // Attack 3
        {
            currentAttackDamage = attack3Damage; // Set damage for this attack
            animator.SetTrigger("isNormal_Attack3");
        }

        if (Input.GetMouseButtonDown(1)) // Right mouse button clicked for charged attack
        {
            isCharging = true;
            animator.SetTrigger("isMaliksi_Charging"); // Trigger to start charging animation
        }

        if (Input.GetMouseButtonUp(1) && isCharging) // Right mouse button released
        {
            currentAttackDamage = chargedAttackDamage; // Set damage for charged attack
            isCharging = false;
            animator.SetTrigger("isMaliksi_ChargeDone"); // Trigger to release attack animation
        }
    }

    public void EnableAttackCollider()
    {
        if (attackCollider != null)
        {
            Debug.Log("Attack collider enabled for attack with damage: " + currentAttackDamage);
            attackCollider.enabled = true;
        }
    }
    public void DisableAttackCollider()
    {
        if (attackCollider != null)
        {
            Debug.Log("Attack collider disabled");
            attackCollider.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Replace "Enemy" with your enemy's tag
        {
            Debug.Log("Hit enemy with attack damage: " + currentAttackDamage);
            OrcMovement enemy = collision.GetComponent<OrcMovement>();
            if (enemy != null)
            {
                enemy.TakeDamage(currentAttackDamage); // Use the current attack damage
            }
        }
    }
}
