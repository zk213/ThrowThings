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
    private Vector2 startPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        startPosition = rb.position;
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

        if (transform.position.sqrMagnitude >= 20 * 20f)
        {
            //rb.position = startPosition;
            //rb.velocity = Vector2.zero;
        }

        //If velocity is over a small number, update "Moving"
        if(Mathf.Abs(rb.velocity.x) >= 0.5)
        {
            GetComponent<Animator>().SetBool("Moving", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Moving", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint2D contact = collision.GetContact(i);
            if (contact.otherCollider.name != "Player" && !contact.otherCollider.GetComponent<Movement>())
            {
                continue;
            }

            float angle = Vector2.Angle(collision.GetContact(i).normal, Vector2.up);
            if (angle < 85f)
            {
                grounded = true;
                nextUnground = Time.time + 0.15f;

                if (collision.GetContact(i).rigidbody)
                {
                    //rb.MovePosition(rb.position + collision.rigidbody.velocity * Time.fixedDeltaTime);
                    rb.velocity += collision.GetContact(i).rigidbody.velocity * 0.1f;
                }
            }
        }
    }
}