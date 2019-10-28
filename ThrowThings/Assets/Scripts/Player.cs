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

public class Player : MonoBehaviour
{
    public GameObject accelerationBarFill;


   
    public GameObject bullet;
    public GameObject newBullet;
    public Transform ray;
    public float angle;

    public GameObject player;

    private bool playerDir = true;

  

    Rigidbody2D rigidbody2DComponent;


    private void Start()
    {
        ray.eulerAngles = new Vector3(0, 0, angle);
    }



    void Awake()
    {
        rigidbody2DComponent = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FireAdjust();
    }


    private void FireAdjust()
    {

        if (Input.GetKey(KeyCode.Space))
        {

            accelerationBarFill.GetComponent<Image>().fillAmount += 0.1f;


        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            float force = accelerationBarFill.GetComponent<Image>().fillAmount;
            force *= 800;

            FireBullet(force);

            accelerationBarFill.GetComponent<Image>().fillAmount = 0;


        }



        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (playerDir)
            {
                angle += 1;
                angle = angle % 360;
            }
            else
            {
                angle -= 1;
                angle = angle % 360;

            }

            ray.eulerAngles = new Vector3(0, 0, angle);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!playerDir)
            {
                angle += 1;
                angle = angle % 360;
            }
            else
            {
                angle -= 1;
                angle = angle % 360;
            }

            ray.eulerAngles = new Vector3(0, 0, angle);

        }

    }

    void FireBullet(float force)
    {

        GameObject newBullet = Instantiate(bullet, player.transform.position, Quaternion.identity) as GameObject;



        Vector2 foreceDirection = new Vector2(Mathf.Cos(Mathf.PI * angle / 180), Mathf.Sin(Mathf.PI * angle / 180));



        newBullet.GetComponent<Rigidbody2D>().AddForce(foreceDirection * force);



    }



}
