using UnityEngine;

public class Holder : MonoBehaviour
{
    [SerializeField]
    private string pressButton = "";

    private Grabbable holdingObject;
    private PhysicsMaterial2D physicsMat;

    private void Awake()
    {
        physicsMat = GetComponentInChildren<Collider2D>().sharedMaterial;
    }

    private void Update()
    {
        if (holdingObject && !Input.GetKey(pressButton))
        {
            Drop();
        }
    }

    private void Drop()
    {
        holdingObject.transform.SetParent(null);
        holdingObject.RestoreRigidbody();
        holdingObject = null;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (holdingObject)
        {
            return;
        }

        if (Input.GetKey(pressButton))
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                ContactPoint2D contact = collision.GetContact(i);
                if (Grabbable.TryGet(contact.collider.transform, out Grabbable grabbable))
                {
                    Grab(grabbable);
                    return;
                }
            }
        }
    }

    private void Grab(Grabbable grabbable)
    {
        holdingObject = grabbable;
        holdingObject.RemoveRigidbody();
        grabbable.transform.SetParent(transform);

        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].sharedMaterial = physicsMat;
        }
    }
}