using UnityEngine;

[ExecuteAlways]
public class WaveyThing : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;

    public bool isSticky = true;

    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private Transform bottom;

    [SerializeField]
    private Rigidbody2D top;

    [SerializeField, Range(-1f, 1f)]
    private float balance = 0f;

    [SerializeField]
    private string team = "";

    [SerializeField]
    private Vector2 spring = new Vector2(1f, 1f);

    private float height;

    public string Team => team;
    public Collider2D[] Colliders { get; private set; } = { };

    public bool CloseEnoughToPlatform(Grabbable grab)
    {
        Bounds otherBounds = grab.Collider.bounds;
        otherBounds.center = new Vector3(otherBounds.center.x, otherBounds.center.y, 0f);

        for (int i = 0; i < Colliders.Length; i++)
        {
            Bounds bounds = Colliders[i].bounds;
            bounds.center = new Vector3(bounds.center.x, bounds.center.y, 0f);
            bounds.Expand(0.15f);

            if (bounds.Intersects(otherBounds))
            {
                return true;
            }
        }

        return false;
    }

    public void RecalculateBalance()
    {
        Colliders = top.GetComponentsInChildren<Collider2D>();
        balance = 0f;
        for (int i = 0; i < Colliders.Length; i++)
        {
            if (Colliders[i].CompareTag("Top"))
            {
                continue;
            }

            float distance = transform.position.x - Colliders[i].transform.position.x;
            balance += distance;
        }

        balance /= Colliders.Length;
        balance *= 0.2f;
        balance = Mathf.Clamp(balance, -1f, 1f);
    }

    public bool Attach(GameObject grab)
    {
        Collider2D col = grab.GetComponentInChildren<Collider2D>();
        if (col.bounds.max.y > top.position.y)
        {
            Rigidbody2D rb = grab.GetComponent<Rigidbody2D>();
            if (rb)
            {
                Destroy(rb);
            }

            grab.transform.SetParent(top.transform);
            RecalculateBalance();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Awake()
    {
        height = top.position.y - bottom.position.y;
        Console.Initialize();
    }

    private void FixedUpdate()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        Vector2 origin = new Vector2(bottom.position.x, bottom.position.y);
        float angle = -((balance * 20f) + 90f) + 180f;
        Vector2 circle = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * height;
        origin += circle;

        Vector2 diff = origin - top.position;
        top.velocity += diff * Time.fixedDeltaTime * top.mass * 3f;

        Vector2 pos = top.position;
        pos.x = Mathf.Lerp(pos.x, origin.x, Time.fixedDeltaTime * top.mass * 0.12f * spring.x);
        pos.y = Mathf.Lerp(pos.y, origin.y, Time.fixedDeltaTime * top.mass * 0.12f * spring.y);
        top.position = pos;

        top.angularVelocity = Mathf.Lerp(top.angularVelocity, 0f, Time.fixedDeltaTime * 1f);
        Vector2 from = (lineRenderer.GetPosition(lineRenderer.positionCount - 1) - lineRenderer.GetPosition(lineRenderer.positionCount - 2)).normalized;
        angle = Mathf.Atan2(from.y, from.x) * Mathf.Rad2Deg - 90;
        top.rotation = Mathf.Lerp(top.rotation, angle, Time.fixedDeltaTime * 6f);
    }

    private void Update()
    {
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float t = Mathf.Clamp01(i / (lineRenderer.positionCount - 1f));
            Vector3 position = Vector3.LerpUnclamped(bottom.position, top.position, t);

            t = curve.Evaluate(t);
            Vector3 lastPosition = lineRenderer.GetPosition(i);
            position = Vector3.Lerp(new Vector3(bottom.position.x, position.y, bottom.position.z), position, t);
            position = Vector3.Lerp(lastPosition, position, 0.5f);
            lineRenderer.SetPosition(i, position);
        }
    }
}