using UnityEngine;

public class Holder : MonoBehaviour
{
    [SerializeField]
    private string pressButton = "";

    [SerializeField]
    private string placeButton = "";

    [SerializeField]
    private bool places = true;

    private float lastHoldTime;
    private Grabbable holdingObject;
    private PhysicsMaterial2D physicsMat;

    private void Awake()
    {
        physicsMat = GetComponentInChildren<Collider2D>().sharedMaterial;

        AttackPlayer attackPlayer = GetComponent<AttackPlayer>();
        if (attackPlayer)
        {
            attackPlayer.enabled = false;
        }
    }

    private void Update()
    {
        if (holdingObject)
        {
            lastHoldTime = Time.time;

            if (Input.GetKeyDown(pressButton))
            {
                Drop();
            }

            if (!string.IsNullOrEmpty(placeButton) && Input.GetKeyDown(placeButton))
            {
                if (places)
                {
                    //check if near the platform
                    if (WaveyThing.CloseEnoughToPlatform(transform.position))
                    {
                        Place();
                    }
                }
                else
                {
                    holdingObject.GetComponent<Collider2D>().enabled = false;
                    AttackPlayer attackPlayer = GetComponent<AttackPlayer>();
                    attackPlayer.bullet = holdingObject.gameObject;
                    attackPlayer.enabled = true;
                    attackPlayer.bullet.transform.SetParent(attackPlayer.target);
                    attackPlayer.bullet.transform.localPosition = Vector2.zero;

                    enabled = false;
                    GetComponent<Movement>().enabled = false;
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    holdingObject = null;
                }
            }
        }
    }

    private void Place()
    {
        holdingObject.transform.SetParent(WaveyThing.Transform);
        holdingObject = null;
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

        if (Time.time > lastHoldTime + 0.5f && Input.GetKey(pressButton))
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