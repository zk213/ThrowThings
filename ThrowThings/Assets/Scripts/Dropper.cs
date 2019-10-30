using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    new GameObject[] Instant;
    int i = -1;

    new GameObject[] Possible;

    [SerializeField]
    private GameObject Obj1;

    [SerializeField]
    private GameObject Obj2;

    [SerializeField]
    private GameObject Obj3;

    [SerializeField]
    private GameObject Obj4;

    float timer = 0;
    float MaxTimer = 3;
    // Start is called before the first frame update
    void Start()
    {
        Instant = new GameObject[5];
        Possible = new GameObject[4];

        Possible[0] = Obj1;
        Possible[1] = Obj2;
        Possible[2] = Obj3;
        Possible[3] = Obj4;
    }

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

            Instant[i] = Instantiate(Possible[Random.Range(0,3)], new Vector3(Random.Range(-5.5f, 5.5f),10,0), Quaternion.identity);
            Instant[i].transform.localScale += new Vector3(Random.Range(0.7f, 1.3f), Random.Range(0.7f, 1.3f), 1);
        }
    }
}
