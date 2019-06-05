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
    public GameObject foreArm;
    public GameObject littleOne;
    public Camera camera;
    public float foreJointAngle;
    public float upperJointAngle;

    float rotateSpeed;
    public float maxRotateSpeed;
    public float foreTarget;

    Vector3 pointerLastPos;

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

        pointerLastPos = Input.mousePosition;
    }

    private void Update()
    {
        //-----Control by keyboard:
        /*if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rotateSpeed = -maxRotateSpeed;
            UpdateMotorSpeed();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rotateSpeed = maxRotateSpeed;
            UpdateMotorSpeed();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            rotateSpeed = 0;
            UpdateMotorSpeed();
        }*/

        //-----Control by mouse:
        /*if (Input.GetAxis("Mouse X") > 0.5)
        {
            rotateSpeed = maxRotateSpeed;
            UpdateMotorSpeed();
        }
        if (Input.GetAxis("Mouse X") < -0.5)
        {
            rotateSpeed = -maxRotateSpeed;
            UpdateMotorSpeed();
        }
        if (Input.GetAxis("Mouse Y") > 0.5  && gameObject.transform.rotation.z < 0)
        {
            rotateSpeed = -maxRotateSpeed;
            UpdateMotorSpeed();
        }
        if (Input.GetAxis("Mouse Y") > 0.5 && gameObject.transform.rotation.z > 0)
        {
            rotateSpeed = maxRotateSpeed;
            UpdateMotorSpeed();
        }
        if (Input.GetAxis("Mouse Y") < -0.5 && gameObject.transform.rotation.z < 0)
        {
            rotateSpeed = maxRotateSpeed;
            UpdateMotorSpeed();
        }
        if (Input.GetAxis("Mouse Y") < -0.5 && gameObject.transform.rotation.z > 0)
        {
            rotateSpeed = -maxRotateSpeed;
            UpdateMotorSpeed();
        }
        if (Input.GetAxis("Mouse Y") < 0.5 && Input.GetAxis("Mouse Y") > -0.5 && Input.GetAxis("Mouse X") < 0.5 && Input.GetAxis("Mouse X") > -0.5)
        {
            rotateSpeed = 0;
            UpdateMotorSpeed();
        }*/

        //print((foreArm.transform.rotation.z/1) * 90);

        TurnArm();

    }

    void UpdateMotorSpeed()
    {
        motorUpper.motorSpeed = rotateSpeed;
        hingeJointUpper.motor = motorUpper;


    }

    void TuneUpperArm1()
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

    void TuneUpperArm2()
    {
        foreTarget = -hingeJointUpper.jointAngle;
        //float error = hingeJointFore.jointAngle - foreTarget;
        //motorFore.motorSpeed = -15 * error;
        hingeJointFore.motor = motorFore;
    }

    void TurnArm()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 forarmPos = foreArm.transform.position;
        Vector3 forarmScreenPos = camera.WorldToScreenPoint(forarmPos);
        Vector3 upperarmScreenPos = camera.WorldToScreenPoint(transform.position);

        float currentAngle = Quaternion.Angle(foreArm.transform.rotation, Quaternion.identity);
        if(gameObject.transform.rotation.z < 0)
        {
            currentAngle = -currentAngle;
        } 

        //print(mousePos + " " + robotScreenPos);

        float expectedAngle = Vector2.SignedAngle(Vector2.up, mousePos - forarmScreenPos);

        print("currentAngle:" + currentAngle + "  " + "expectedAngle:" + expectedAngle);

        //Force depends on angleDifferent, decrease the denominator to increase the force
        float angleDifferent = Mathf.Abs(expectedAngle - currentAngle)/120;

        if(-170 < expectedAngle && expectedAngle < 170)
        {
            if (expectedAngle > currentAngle)
            {
                rotateSpeed = -maxRotateSpeed * angleDifferent;
            }
            else
            {
                rotateSpeed = maxRotateSpeed * angleDifferent;
            }

            TuneUpperArm1();
            UpdateMotorSpeed();
        }
        if(-170 >= expectedAngle || expectedAngle >= 170)
        {
            Vector3 pointerCurrentPos = mousePos;
            float angle = Vector2.SignedAngle(pointerLastPos, pointerCurrentPos);
            float deltaAngle = Vector2.Angle(mousePos, upperarmScreenPos)/120;
            if (angle >= 0)
            {
                rotateSpeed = maxRotateSpeed * deltaAngle;
            }
            else
            {
                rotateSpeed = -maxRotateSpeed * deltaAngle;
            }

            TuneUpperArm2();
            UpdateMotorSpeed();
        }

        pointerLastPos = mousePos;


    }



    void StopMotors()
    {
        motorUpper.motorSpeed = 0;
        motorFore.motorSpeed = 0;

        hingeJointUpper.motor = motorUpper;
        hingeJointFore.motor = motorFore;

    }
}
