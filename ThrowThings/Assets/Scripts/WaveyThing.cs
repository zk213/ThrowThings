using UnityEngine;

public class WaveyThing : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private Transform @base;

    [SerializeField]
    private Transform top;

    private Vector3 velocity;

    private void Awake()
    {
        Console.Initialize();
    }

    private void FixedUpdate()
    {
        Vector3 origin = new Vector3(@base.position.x, top.position.y, @base.position.z);
        Vector3 diff = origin - top.position;
        velocity += diff * Time.fixedDeltaTime * 4f;
        top.position += velocity;
        top.position = Vector3.Lerp(top.position, origin, Time.fixedDeltaTime * 5f);
        top.LookAt(@base.position, Vector3.left);

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float t = Mathf.Clamp01(i / (lineRenderer.positionCount - 1f));
            Vector3 position = Vector3.LerpUnclamped(@base.position, top.position, t);

            t = curve.Evaluate(t);
            position.x *= t;
            position.z *= t;

            lineRenderer.SetPosition(i, position);
        }
    }
}