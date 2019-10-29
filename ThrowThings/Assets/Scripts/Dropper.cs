using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    new GameObject[] Balls;
    int i = -1;

    [SerializeField]
    private GameObject Ball;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        Balls = new GameObject[5];  
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 10)
        {
            timer = 0;
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
