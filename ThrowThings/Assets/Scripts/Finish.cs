﻿using UnityEngine;

public class Finish : MonoBehaviour
{
    public static void IgnoreCollision(Collider2D collider, bool ignore)
    {
        Collider2D finishCollider = FindObjectOfType<Finish>().GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(finishCollider, collider, ignore);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        WaveyThing thing = collision.collider.GetComponentInParent<WaveyThing>();
        if (thing)
        {
            //win condition here
            GameManager.TeamWin(thing.Team);
        }
    }
}
