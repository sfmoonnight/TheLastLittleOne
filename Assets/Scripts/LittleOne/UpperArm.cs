using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperArm : MonoBehaviour
{
    public HingeJoint2D hingeJointUpper;
    public HingeJoint2D hingeJointFore;
    FixedJoint2D fixedJoint;
    JointMotor2D motorUpper;
    JointMotor2D motorFore;
    public GameObject foreArmPivot;
    public float foreJointAngle;
    public float upperJointAngle;

    float rotateSpeed;
    public float maxRotateSpeed;
    public float foreTarget;

    //Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        //Physics2D.IgnoreLayerCollision(8, 8);
        //rb = GetComponent<Rigidbody2D>();
        //fixedJoint = GetComponent<FixedJoint2D>();
        //hingeJointUpper = GetComponent<HingeJoint2D>();
        motorUpper = hingeJointUpper.motor;
        motorFore = hingeJointFore.motor;
        motorUpper.motorSpeed = 0;
        motorFore.motorSpeed = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            rotateSpeed = -maxRotateSpeed;
            UpdateMotorSpeed();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rotateSpeed = maxRotateSpeed;
            UpdateMotorSpeed();
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            rotateSpeed = 0;
            UpdateMotorSpeed();
        }



        tuneUpperArm();
    }

    void UpdateMotorSpeed()
    {
        motorUpper.motorSpeed = rotateSpeed;
        hingeJointUpper.motor = motorUpper;


    }

    void tuneUpperArm()
    {
        foreJointAngle = hingeJointFore.jointAngle;
        upperJointAngle = hingeJointUpper.jointAngle;

        foreTarget = hingeJointUpper.jointAngle * 2;
        float error = hingeJointFore.jointAngle - foreTarget;
        motorFore.motorSpeed = -15*error;
        /*
        if (hingeJointFore.jointAngle - foreTarget < 10)
        {
            motorFore.motorSpeed = maxRotateSpeed;
        }
        else if (hingeJointFore.jointAngle - foreTarget > 10)
        {
            motorFore.motorSpeed = -maxRotateSpeed;
        }
        else
        {
            motorFore.motorSpeed = 0;
        }*/
        hingeJointFore.motor = motorFore;
    }


    void RotateArmClockwise()
    {
        //print(hingeJointUpper.transform.rotation.z);


        //if (hingeJointUpper.transform.rotation.z < 0.7)
        //{
            //transform.Rotate(Vector3.forward);
            motorUpper.motorSpeed = -rotateSpeed;
            hingeJointUpper.motor = motorUpper;

            //foreArmPivot.transform.Rotate(Vector3.forward);
            //motorFore.motorSpeed = -rotateSpeed;
            //hingeJointFore.motor = motorFore;
        //}

        //Mathf.Clamp(transform.rotation.eulerAngles.z, -90f, 90f);
        //motor.motorSpeed = 5;
        //rb.AddTorque(5);
    }

    void RotateArmConterClockwise()
    {
        //print("rotating");
        //print(hingeJointUpper.transform.rotation.z);
       //if (hingeJointUpper.transform.rotation.z > -0.7)
        //{
            //transform.Rotate(Vector3.back);
            motorUpper.motorSpeed = rotateSpeed;
            hingeJointUpper.motor = motorUpper;

            //foreArmPivot.transform.Rotate(Vector3.back);
            //motorFore.motorSpeed = rotateSpeed;
            //hingeJointFore.motor = motorFore;
        //}

        //Mathf.Clamp(transform.rotation.z, -0.25f, 0.25f);
        //motor.motorSpeed = 5;
        //rb.AddTorque(5);
    }

    void StopMotors()
    {
        motorUpper.motorSpeed = 0;
        motorFore.motorSpeed = 0;

        hingeJointUpper.motor = motorUpper;
        hingeJointFore.motor = motorFore;

    }
}
