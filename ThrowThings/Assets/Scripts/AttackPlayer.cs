/**
 *  @Player.cs
 *  @version: 1.00
 *  @author: Jesse Freeman
 *  @date: Feb 3
 *  @copyright (c) 2012 Jesse Freeman, under The MIT License (see LICENSE)
 * 
 * 	-- For use with Weekend Code Project: Unity's New 2D Workflow book --
 *
 *  This script will allow you to move a GameObject, idealy the player,
 *  via the keyboard and the mouse. This will slide GameObject forward
 *  based on the direction of the input.
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField]
    private string VerticalInput;
    [SerializeField]

    private string Fire;
    [SerializeField]

    private string HorizontalInput;


    public GameObject accelerationBarFill;



    public GameObject bullet;
    public GameObject newBullet;
    public Transform target;

    public GameObject player;




    Rigidbody2D rigidbody2DComponent;



    float UpAndDown;
    float LeftAndRight;

    private void Start()
    {

        target.transform.position = this.transform.position;
    }



    void Awake()
    {
        rigidbody2DComponent = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpAndDown = Input.GetAxisRaw(VerticalInput);
        LeftAndRight = Input.GetAxisRaw(HorizontalInput);
        FireAdjust();
    }


    private void FireAdjust()
    {


        if (Input.GetButton(Fire))
        {

            accelerationBarFill.GetComponent<Image>().fillAmount = Mathf.PingPong(Time.time, 1f);


        }
        if (Input.GetButtonUp(Fire))
        {
            float force = accelerationBarFill.GetComponent<Image>().fillAmount;
            force *= 800;

            FireBullet(force);

            accelerationBarFill.GetComponent<Image>().fillAmount = 0;


        }





        target.transform.position = (Vector2)transform.position + new Vector2(LeftAndRight, UpAndDown).normalized * 1.5f;






    }

    void FireBullet(float force)
    {


        Vector2 foreceDirection = new Vector2(LeftAndRight, UpAndDown).normalized;
        bool shouldIgnore = Vector2.Angle(Vector2.down, foreceDirection) < 90;
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>(), shouldIgnore);
        bullet.GetComponent<Collider2D>().enabled = true;


        bullet.GetComponent<Grabbable>().RestoreRigidbody();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(foreceDirection * force * rb.mass);
        GetComponent<Movement>().enabled = true;
        GetComponent<Holder>().enabled = true;
        bullet.transform.SetParent(null);
        enabled = false;


    }



}
