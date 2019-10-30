using UnityEngine;

public class StickToPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.attachedRigidbody && collision.collider.attachedRigidbody.name == "Top")
        {
            WaveyThing thing = collision.collider.GetComponentInParent<WaveyThing>();
            enabled = !thing.Attach(gameObject);
            if (enabled)
            {
                Finish.IgnoreCollision(GetComponent<Collider2D>(), false);
            }
        }
    }
}