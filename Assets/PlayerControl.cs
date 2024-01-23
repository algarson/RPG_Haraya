using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float movSpeed;
    float speedX, speedY;
    Rigidbody2D rb;

    [SerializeField] public LayerMask solidObjectsLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movSpeed;

        // Update the velocity based on the input
        rb.velocity = new Vector2(speedX, speedY);

        // Check for walkability and initiate movement
        Vector3 targetPos = transform.position + new Vector3(speedX, speedY, 0f);
        if (IsWalkable(targetPos))
        {
            StartCoroutine(Move(targetPos));
        }

        // Flip the character's sprite based on movement direction
        FlipCharacter();
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
        {
            return false;
        }
        return true;
    }

    private IEnumerator Move(Vector3 targetPos)
    {
        // Add any movement-related logic here (e.g., animations)
        yield return null;
    }

    private void FlipCharacter()
    {
        // Flip the character's sprite based on the horizontal movement direction
        if (speedX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Flip to face left
        }
        else if (speedX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
        // If speedX is 0, do not change the facing direction
    }
}
