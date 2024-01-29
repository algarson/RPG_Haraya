using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_atk : MonoBehaviour
{
    public Collider2D attackCollider; // Assign this in the Inspector
    public int attackDamage = 2; // Example value, adjust as needed
    public void EnableAttackCollider()
    {
        if (attackCollider != null)
        {
            Debug.Log("Enabling attack collider with damage: " + attackDamage);
            attackCollider.enabled = true;
        }
    }
    public void DisableAttackCollider()
    {
        Debug.Log("Disabling attack");
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Replace "Enemy" with your enemy's tag
        {
            OrcMovement enemy = collision.GetComponent<OrcMovement>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage); // Define attackDamage as per your game's logic
            }
        }
    }
}
