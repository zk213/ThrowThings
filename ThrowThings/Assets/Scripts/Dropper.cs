using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    int i = -1;

    new GameObject[] Instant = new GameObject[5];

    [SerializeField]
    new GameObject[] Objs = new GameObject[4];

    float timer = 0;
    float MaxTimer = 3;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= MaxTimer)
        {
            MaxTimer = Random.Range(3,5);

            timer = 0;
            i++;
            if(i >= 5)
            {
                i = 0;
            }

            // if Balls[i] is not null, destroy Balls[i]
            if(Instant[i] != null)
            {
                Destroy(Instant[i]);
            }

            Instant[i] = Instantiate(Objs[Random.Range(0,3)], new Vector3(Random.Range(-5.5f, 5.5f),10,0), Quaternion.identity);
            Instant[i].transform.localScale += new Vector3(Random.Range(0.7f, 1.3f), Random.Range(0.7f, 1.3f), 1);
        }
    }
}
