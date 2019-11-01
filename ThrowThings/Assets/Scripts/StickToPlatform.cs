using UnityEngine;

public class StickToPlatform : MonoBehaviour
{
    public string team;

    [SerializeField]
    private AudioClip hitSound;

    [SerializeField]
    private AudioClip stickSound;

    private AudioSource source;
    private bool stuck;

    private void Awake()
    {
        source = GetComponentInChildren<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        WaveyThing thing = collision.collider.GetComponentInParent<WaveyThing>();
        if (thing && thing.isSticky)
        {
            bool wasStuck = false;
            stuck = thing.Attach(gameObject);
            if (stuck)
            {
                if (!wasStuck)
                {
                    source.PlayOneShot(stickSound);
                    source.pitch = 1.25f;
                    source.volume = 0.3f;
                }

                Finish.IgnoreCollision(GetComponent<Collider2D>(), false);
                return;
            }
        }

        source.pitch = 1f;
        source.volume = Mathf.Clamp(collision.relativeVelocity.magnitude * 0.02f, 0.08f, 0.3f);
        source.PlayOneShot(hitSound);
    }
}
