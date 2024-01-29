using UnityEngine;

public class MaliksiSpriteLayering : MonoBehaviour
{
    private SpriteRenderer MaliksispriteRenderer;

    void Start()
    {
        MaliksispriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Lower Y position (further down the screen) means a higher order in layer (drawn on top).
        MaliksispriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }
}