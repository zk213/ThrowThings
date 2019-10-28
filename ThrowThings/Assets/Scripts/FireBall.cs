using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    void Start()
    {
        Invoke("SelfDestruct", 3);


    }



    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Missile")
        {

            Destroy(gameObject);
        }
    }
    void SelfDestruct()
    {

        Destroy(gameObject);

    }
}
