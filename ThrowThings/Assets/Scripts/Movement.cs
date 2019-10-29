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
    private string verticalInput = "Vertical2";

    [SerializeField]
    private bool allowKeyboard = false;

    [SerializeField]
    private float jumpHeight = 4f;

    private Rigidbody2D rb;
    private bool grounded;
    private float nextUnground;

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

            if (grounded && Input.GetKeyDown(KeyCode.Space))
            {
                grounded = false;
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(2f * jumpHeight * gravity));
            }
        }

        if (grounded && Input.GetAxisRaw(verticalInput) > 0.5f)
        {
            grounded = false;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(2f * jumpHeight * gravity));
        }

        Vector2 velocity = rb.velocity;
        velocity.x = Mathf.Lerp(velocity.x, direction * speed, Time.fixedDeltaTime * accel);
        rb.velocity = velocity;
        rb.AddForce(gravity * Vector2.down * rb.mass);

        if (Time.time > nextUnground)
        {
            grounded = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.rigidbody)
        {
            //rb.MovePosition(rb.position + collision.rigidbody.velocity * Time.fixedDeltaTime);
            rb.velocity += collision.rigidbody.velocity * 0.15f;
        }

        for (int i = 0; i < collision.contactCount; i++)
        {
            if (Vector2.Angle(collision.GetContact(i).normal, Vector2.up) < 60f)
            {
                grounded = true;
                nextUnground = Time.time + 0.15f;
            }
        }
    }
}