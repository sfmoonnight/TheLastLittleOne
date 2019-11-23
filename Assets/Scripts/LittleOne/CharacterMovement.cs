using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody2D character;
    public float speed;
    public bool onGround;
    public Toggle enableAD;

    StateManager stateManager;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Rigidbody2D>();
        stateManager = Toolbox.GetInstance().GetStateManager();
    }

    // Update is called once per frame
    void Update()
    {
        if (stateManager.GetTmpState().preventGameInput)
        {
            return;
        }

        if (enableAD.isOn)
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
    }

    void Move(float force)
    {
        //print("IN");
        if (onGround)
        {
            character.AddForce(Vector2.right * force);
        }
        //character.velocity = Vector2.right * force;

        //character.position = character.position + Vector2.right * force;
    }

}
