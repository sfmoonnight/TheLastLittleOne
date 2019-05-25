using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody2D character;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Move(-speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move(speed);
        }
    }

    void Move(float force)
    {
        print("IN");
        //character.velocity = Vector2.right * force;
        character.AddForce(Vector2.right * force);
        //character.position = character.position + Vector2.right * force;
    }
}
