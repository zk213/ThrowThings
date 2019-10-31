using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicTranslation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
        if (gameObject.transform.root.gameObject.GetComponent<Rigidbody2D>().velocity.x >= 0.5)
        {
            GetComponent<Animator>().SetBool("Moving", true);
        }
        GetComponent<Animator>().SetBool("Moving", false);
    }
}
