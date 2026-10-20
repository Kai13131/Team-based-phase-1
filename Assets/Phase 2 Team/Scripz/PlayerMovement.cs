using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // Ensuring we can't start the game without attaching Rigidbody to applied character.
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]

    public float moveSpeed = 6f;
    public float acceleration = 45f;
    public float deceleration = 55;

    private Rigidbody2D rb;

    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // WASD
        // We're grabbing from Unity's Input System.

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Preventing diagonal movement from being too fast.

        moveInput = new Vector2(h, v).normalized;
    }

    private void FixedUpdate()
    {
        // Grabs initial movement speed and calculates it.

        Vector2 targetVelocity = moveInput * moveSpeed;

        // Floats accelerations to ensure if player needs to move fast or slow.

        float currentAcceleration = moveInput.sqrMagnitude > 0f ? acceleration : deceleration; 

        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, targetVelocity, currentAcceleration * Time.fixedDeltaTime);

    }

}
