using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStepdown : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (rb.velocity.magnitude >= 0.5)
        {
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
    }
}
