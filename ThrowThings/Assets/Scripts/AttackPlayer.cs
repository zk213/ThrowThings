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
using UnityEngine.UI;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField]
    private string team = "";

    [SerializeField]
    private string VerticalInput;

    [SerializeField]
    private string HorizontalInput;

    [SerializeField]
    private string FireKey = "";

    public Transform grabRoot;
    public GameObject accelerationBarFill;



    public GameObject bullet;
    public GameObject newBullet;

    public GameObject player;






    private LineRenderer line;
    float UpAndDown;
    float LeftAndRight;


    void Awake()
    {
        line = GetComponentInChildren<LineRenderer>();
        line.enabled = false;
    }

    private void OnEnable()
    {
        line.enabled = true;
    }

    private void OnDisable()
    {
        line.enabled = false;
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


        if (Input.GetKey(FireKey))
        {

            accelerationBarFill.GetComponent<Image>().fillAmount = Mathf.PingPong(Time.time, 1f);


        }
        if (Input.GetKeyUp(FireKey))
        {
            float force = accelerationBarFill.GetComponent<Image>().fillAmount;
            force *= 800;

            FireBullet(force);

            accelerationBarFill.GetComponent<Image>().fillAmount = 0;


        }





        line.SetPosition(0, transform.position);
        line.SetPosition(1, (Vector2)transform.position + new Vector2(LeftAndRight, UpAndDown).normalized * 100f);
        grabRoot.position = (Vector2)transform.position + new Vector2(LeftAndRight, UpAndDown).normalized * 1.8f;





    }

    void FireBullet(float force)
    {


        Vector2 foreceDirection = new Vector2(LeftAndRight, UpAndDown).normalized;
        bool shouldIgnore = Vector2.Angle(Vector2.down, foreceDirection) < 90;
        Finish.IgnoreCollision(bullet.GetComponent<Collider2D>(), true);
        bullet.GetComponent<Collider2D>().enabled = true;
        bullet.GetComponent<StickToPlatform>().team = team;


        bullet.GetComponent<Grabbable>().RestoreRigidbody();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(foreceDirection * force * rb.mass);
        GetComponent<Movement>().enabled = true;
        GetComponent<Holder>().enabled = true;
        bullet.transform.SetParent(null);
        enabled = false;


    }



}
