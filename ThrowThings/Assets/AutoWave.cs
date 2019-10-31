using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWave : MonoBehaviour
{
    [SerializeField]
    private float timeOffset = 0.5f;

    [SerializeField]
    private AnimationCurve yCurve;

    [SerializeField]
    private float amount = 0.2f;

    [SerializeField]
    private float speed = 1f;

    private float y;
    private float time;

    private void Awake()
    {
        y = transform.position.y;
        time += timeOffset;
    }

    private void Update()
    {
        time += Time.deltaTime * speed;
        Vector3 pos = transform.position;
        pos.y = Mathf.Lerp(y - amount, y + amount, yCurve.Evaluate(Mathf.PingPong(time, 1f)));
        transform.position = pos;
    }
}
