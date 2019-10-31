using System;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private float oldMass;
    private RigidbodyInterpolation2D oldInterp;

    public static List<Grabbable> Grabbables { get; set; } = new List<Grabbable>();

    public Rigidbody2D Rigidbody { get; private set; }
    public Collider2D Collider { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponentInChildren<Collider2D>();
    }

    private void OnEnable()
    {
        Grabbables.Add(this);
    }

    private void OnDisable()
    {
        Grabbables.Remove(this);
    }

    public static bool TryGet(Transform transform, out Grabbable grabbable)
    {
        for (int i = 0; i < Grabbables.Count; i++)
        {
            if (transform.IsChildOf(Grabbables[i].transform))
            {
                grabbable = Grabbables[i];
                return true;
            }
        }

        grabbable = null;
        return false;
    }

    public void RestoreRigidbody()
    {
        if (!Rigidbody)
        {
            Rigidbody = gameObject.AddComponent<Rigidbody2D>();
        }

        Rigidbody.mass = oldMass;
        Rigidbody.interpolation = oldInterp;
    }

    public void RemoveRigidbody()
    {
        if (Rigidbody)
        {
            oldMass = Rigidbody.mass;
            oldInterp = Rigidbody.interpolation;
        }

        Destroy(Rigidbody);
    }

    public bool IsCloseEnough(Bounds bounds)
    {
        Bounds b = Collider.bounds;
        b.center = new Vector3(b.center.x, b.center.y, 0f);
        b.Expand(0.15f);

        bounds.center = new Vector3(bounds.center.x, bounds.center.y, 0f);
        return b.Intersects(bounds);
    }
}