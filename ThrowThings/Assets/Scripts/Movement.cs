using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float accel = 8f;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float gravity = 20f;

    [SerializeField]
    private string horizontalInput = "Horizontal1";

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void FixedUpdate()
    {
        float direction = Input.GetAxisRaw(horizontalInput);
        Vector2 velocity = rb.velocity;
        velocity.x = Mathf.Lerp(velocity.x, direction * speed, Time.fixedDeltaTime * accel);
        rb.velocity = velocity;
        rb.AddForce(gravity * Vector2.down * rb.mass);
    }
}