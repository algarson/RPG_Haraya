using UnityEngine;

public class SpriteLayering : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Lower Y position (further down the screen) means a higher order in layer (drawn on top).
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }
}