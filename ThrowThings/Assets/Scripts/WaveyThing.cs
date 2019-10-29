using UnityEngine;

[ExecuteAlways]
public class WaveyThing : MonoBehaviour
{
    private static WaveyThing instance;

    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private Transform bottom;

    [SerializeField]
    private Rigidbody2D top;

    [SerializeField, Range(-1f, 1f)]
    private float balance = 0f;

    private float height;

    public static Transform Transform
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<WaveyThing>();
            }

            return instance.top.transform;
        }
    }

    public static bool CloseEnoughToPlatform(Vector2 position)
    {
        if (!instance)
        {
            instance = FindObjectOfType<WaveyThing>();
        }

        if (!instance)
        {
            return false;
        }

        Bounds bounds = new Bounds(instance.top.position, instance.top.transform.localScale);
        bounds.Expand(2f);

        return bounds.Contains(position);
    }

    private void Awake()
    {
        height = top.position.y - bottom.position.y;
        Console.Initialize();
    }

    private void OnEnable()
    {
        instance = this;
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
        top.position = Vector3.Lerp(top.position, origin, Time.fixedDeltaTime * top.mass * 0.12f);

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