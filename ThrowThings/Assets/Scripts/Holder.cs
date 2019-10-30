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
    private WaveyThing[] things = { };

    private void Awake()
    {
        things = FindObjectsOfType<WaveyThing>();
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
                    for (int i = 0; i < things.Length; i++)
                    {
                        if (things[i].CloseEnoughToPlatform(holdingObject))
                        {
                            Place(things[i]);
                            break;
                        }
                    }
                }
                else
                {
                    Collider2D collider = holdingObject.GetComponent<Collider2D>();
                    if (collider)
                    {
                        collider.enabled = false;
                    }

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
        else
        {
            if (Input.GetKey(pressButton) && Time.time > lastHoldTime + 0.5f)
            {
                for (int i = 0; i < Grabbable.Grabbables.Count; i++)
                {
                    Grabbable grab = Grabbable.Grabbables[i];
                    if (grab.IsCloseEnough(transform.position))
                    {
                        Grab(grab);
                        break;
                    }
                }
            }
        }
    }

    private void Place(WaveyThing thing)
    {
        thing.Attach(holdingObject.gameObject);
        holdingObject = null;
        gameObject.GetComponent<Animator>().SetBool("Holding", false);
    }

    private void Drop()
    {
        holdingObject.transform.SetParent(null);
        holdingObject.RestoreRigidbody();
        holdingObject = null;
        gameObject.GetComponent<Animator>().SetBool("Holding", false);
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
        gameObject.GetComponent<Animator>().SetBool("Holding", true);
    }
}