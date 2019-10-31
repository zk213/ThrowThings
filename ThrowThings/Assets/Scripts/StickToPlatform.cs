using UnityEngine;

public class StickToPlatform : MonoBehaviour
{
    public int team;
    
    private bool stuck;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!stuck)
        {
            StickToPlatform stick = collision.collider.GetComponentInParent<StickToPlatform>();
            if (stick)
            {
                if (transform.position.y < Finish.Y)
                {
                    if (team != stick.team)
                    {
                        Destroy(gameObject);
                        Destroy(stick.gameObject);
                    }
                    else
                    {
                        transform.SetParent(stick.transform);
                        Rigidbody2D rb = GetComponent<Rigidbody2D>();
                        if (rb)
                        {
                            Destroy(rb);
                        }

                        stuck = true;
                        Finish.IgnoreCollision(GetComponent<Collider2D>(), false);
                    }

                    return;
                }
            }
            
            WaveyThing thing = collision.collider.GetComponentInParent<WaveyThing>();
            stuck = thing.Attach(gameObject);
            if (stuck)
            {
                Finish.IgnoreCollision(GetComponent<Collider2D>(), false);
            }
        }
    }
}
