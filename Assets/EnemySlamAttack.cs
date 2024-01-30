using UnityEngine;

public class EnemySlamAttack : MonoBehaviour
{
    public int slamDamage = 5; // Set the damage value for the slam attack

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Make sure "Player" is the tag of your player game object
        {
            Player_Control playerControl = collision.GetComponent<Player_Control>();
            if (playerControl != null)
            {
                playerControl.TakeDamage(slamDamage);
            }
        }
    }
}
