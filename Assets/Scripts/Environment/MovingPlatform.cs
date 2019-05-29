using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum Direction {left, right, up, down};

    public Direction direction;
    public float speed;

    public List<GameObject> inhabitant;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        inhabitant = new List<GameObject>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 dpos = Vector3.zero;
        //Vector2 force = Vector2.zero;
        if(direction == Direction.left)
        {
            dpos = Vector3.left * speed * Time.deltaTime;
            //force = Vector2.left * 500;
        }
        if (direction == Direction.right)
        {
            dpos = Vector3.right * speed * Time.deltaTime;
        }
        //transform.Translate(dpos);
        rb.velocity = dpos;

        foreach (GameObject inhab in inhabitant)
        {
            //inhab.transform.Translate(dpos);
            //inhab.GetComponent<Rigidbody2D>().AddForce(Vector2.right );
        }
    }
}
