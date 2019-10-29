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

    [SerializeField]
    private bool allowKeyboard = false;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void FixedUpdate()
    {
        float direction = Input.GetAxisRaw(horizontalInput);
        if (allowKeyboard)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                direction = -1f;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                direction = 1f;
            }
        }

        Vector2 velocity = rb.velocity;
        velocity.x = Mathf.Lerp(velocity.x, direction * speed, Time.fixedDeltaTime * accel);
        rb.velocity = velocity;
        rb.AddForce(gravity * Vector2.down * rb.mass);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.rigidbody)
        {
            //rb.MovePosition(rb.position + collision.rigidbody.velocity * Time.fixedDeltaTime);
            rb.velocity += collision.rigidbody.velocity * 0.15f;
        }
    }
}