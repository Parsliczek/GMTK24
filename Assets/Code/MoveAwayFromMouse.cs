using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAwayFromMouse : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the sphere's movement
    public float safeDistance = 3f; // Distance within which the sphere will start moving away
    public float slideFriction = 0.98f; // Controls how fast the sphere slows down after moving (higher value for more sliding)

    private Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody2D component attached to the sphere
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Ensure no gravity affects the sphere's movement
    }

    void Update()
    {
        // Get the mouse position in world coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the distance between the sphere and the mouse cursor
        float distance = Vector2.Distance(mousePosition, rb.position);

        if (distance < safeDistance)
        {
            // Calculate the direction to move away from the mouse
            Vector2 directionAwayFromMouse = (rb.position - mousePosition).normalized;

            // Move the sphere in the opposite direction of the mouse
            rb.velocity = directionAwayFromMouse * moveSpeed;
        }
        else
        {
            // Gradually reduce the velocity to create a longer sliding effect
            rb.velocity = rb.velocity * slideFriction;

            // Stop the sphere completely if its velocity is very low
            if (rb.velocity.magnitude < 0.05f) // Reduced threshold for smoother stopping
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}
