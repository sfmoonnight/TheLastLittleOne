using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmControl : MonoBehaviour
{
    public Camera camera;

    public GameObject foreArm;
    public GameObject upperArm;
    JointMotor2D motorUpper;
    JointMotor2D motorFore;
    public HingeJoint2D hingeJointUpper;
    public HingeJoint2D hingeJointFore;
    public float maxRotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        motorUpper = hingeJointUpper.motor;
        motorFore = hingeJointFore.motor;
        motorUpper.motorSpeed = 0;
        motorFore.motorSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PidForeArm();
        PidUpperArm();
    }

    Vector2 GetMousePosition()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
        return mouseWorldPos;
    }

    void PidForeArm()
    {
        /*
         * Ensure the fore arm aligns with mouse
         * 
         */

        Vector2 mouseWorldPos = GetMousePosition();
        Vector2 forearmPos = foreArm.transform.position;

        // This line differs between fore arm and upper arm
        Vector2 expectedDirection = (mouseWorldPos - forearmPos).normalized;

        Vector2 currentDirection = foreArm.transform.up;
        float deltaAngle = Vector2.SignedAngle(currentDirection, expectedDirection);
        float rotateSpeed = -maxRotateSpeed * deltaAngle;

        motorFore.motorSpeed = rotateSpeed;
        hingeJointFore.motor = motorFore;
    }

    void PidUpperArm()
    {
        /*
         * Ensure upper arm aligns with target 
         */

        Vector2 mouseWorldPos = GetMousePosition();

        // This line differs between fore arm and upper arm
        Vector2 expectedDirection = ComputeUpperArmTarget();

        Vector2 currentDirection = upperArm.transform.up;
        float deltaAngle = Vector2.SignedAngle(currentDirection, expectedDirection);
        float rotateSpeed = -maxRotateSpeed * deltaAngle;

        motorUpper.motorSpeed = rotateSpeed;
        hingeJointUpper.motor = motorUpper;
    }

    Vector2 ComputeUpperArmTarget()
    {
        /*
         * Calculate upper arm target 
         */

        Vector2 mouseWorldPos = GetMousePosition();
        Vector2 upperArmPos = upperArm.transform.position;
        Vector2 pointerDirection = (mouseWorldPos - upperArmPos).normalized;
        Vector2 expectedDirection = new Vector2();

        float pointerAngle = Vector2.SignedAngle(Vector2.up, pointerDirection);

        //print(pointerAngle);

        if (pointerAngle <= 120 || pointerAngle >= -120)
        {
            float expectedAngle = pointerAngle * 0.5f;
            expectedDirection = RotateVector2ByAngle(Vector2.up, expectedAngle);
        }
        
        if(pointerAngle > 120 || pointerAngle < -120)
        {
            //print("Im Lower part");
            float expectedAngle = (Mathf.Abs(pointerAngle)/180) * pointerAngle;
            //print(expectedAngle);
            expectedDirection = RotateVector2ByAngle(Vector2.up, expectedAngle);
        }

        

        return expectedDirection;
    }

    Vector2 RotateVector2ByAngle(Vector2 startingVector, float angle)
    {
        angle *= Mathf.Deg2Rad;
        float x = startingVector.x * Mathf.Cos(angle) - startingVector.y * Mathf.Sin(angle);
        float y = startingVector.x * Mathf.Sin(angle) + startingVector.y * Mathf.Cos(angle);

        return new Vector2(x, y);
    }

}
