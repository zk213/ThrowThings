using UnityEngine;

public class StickToPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.attachedRigidbody && collision.collider.attachedRigidbody.name == "Top")
        {
            WaveyThing thing = collision.collider.GetComponentInParent<WaveyThing>();
            thing.Attach(gameObject);
            enabled = false;
        }
    }
}