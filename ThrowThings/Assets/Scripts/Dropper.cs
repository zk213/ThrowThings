using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    new GameObject[] Balls;
    int i = -1;

    [SerializeField]
    private GameObject Ball;

    // Start is called before the first frame update
    void Start()
    {
        Balls = new GameObject[5];  
    }

    // Update is called once per frame
    void Update()
    {
        // i increases each time. When it reaches 5, it resets to 0.

        // Check the array's position before instantiating, if it is not null, delete the object.
        if (Input.GetKeyDown("space"))
        {
            i++;
            if(i >= 5)
            {
                i = 0;
            }

            // if Balls[i] is not null, destroy Balls[i]
            if(Balls[i] != null)
            {
                Destroy(Balls[i]);
            }

            Balls[i] = Instantiate(Ball, gameObject.transform.position, Quaternion.identity);
        }
    }
}
