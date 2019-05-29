using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSenser : MonoBehaviour
{
    GameObject body;

    public Vector3 initPos;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        body = GameObject.Find("body");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MovingObstacle"))
        {
            collision.GetComponent<MovingPlatform>().inhabitant.Add(gameObject.transform.root.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            //print("collide");
            body.GetComponent<CharacterMovement>().onGround = true;
        }
        if (collision.CompareTag("MovingObstacle"))
        {
            //initPos = collision.transform.position;
            print("moving");
            body.GetComponent<CharacterMovement>().onGround = true;
            //collision.GetComponent<FrictionJoint2D>().connectedBody = body.GetComponent<Rigidbody2D>();
            //offset = collision.transform.position - initPos;
            //body.transform.parent.SetParent(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            body.GetComponent<CharacterMovement>().onGround = false;
        }
        if (collision.CompareTag("MovingObstacle"))
        {
            body.GetComponent<CharacterMovement>().onGround = false;
            //collision.GetComponent<FrictionJoint2D>().connectedBody = null;

            body.transform.parent.SetParent(null);
        }
    }

    void LateUpdate()
    {
        //if (offset != Vector3.zero)
        //{
            //body.transform.parent.position = body.transform.parent.position + offset;
        //}
    }
}
