using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeArm : MonoBehaviour
{
    //HingeJoint2D hingeJoint;
    FixedJoint2D fixedJoint;
    JointMotor2D motor;
    //Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        //Physics2D.IgnoreLayerCollision(8, 8);
        //rb = GetComponent<Rigidbody2D>();
        //fixedJoint = GetComponent<FixedJoint2D>();
        //hingeJoint = GetComponent<HingeJoint2D>();
        //motor = hingeJoint.motor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            RotateArm();
        }
    }

    void RotateArm()
    {
        print("rotating");
        transform.Rotate(Vector3.forward);
        //motor.motorSpeed = 5;
        //rb.AddTorque(5);
    }
}
