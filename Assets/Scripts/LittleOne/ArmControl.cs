using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmControl : MonoBehaviour
{
    public Camera camera;
    public GameObject body; // not in use
    public GameObject foreArm;
    public GameObject upperArm;
    public JointMotor2D motorUpper;
    public JointMotor2D motorFore;
    public HingeJoint2D hingeJointUpper;
    public HingeJoint2D hingeJointFore;
    public float maxRotateSpeedFore;
    public float maxRotateSpeedUpper;
    public float foreArmCoeffD; // not in use
    public Collider2D bodyCollider;

    bool armDiverging = false;
    Vector2 currMouseWorldPos;
    Queue<float> foreArmErrorQueue;

    // Start is called before the first frame update
    void Start()
    {
        foreArmErrorQueue = new Queue<float>();
        currMouseWorldPos = GetMousePosition();
        motorUpper = hingeJointUpper.motor;
        motorFore = hingeJointFore.motor;
        motorUpper.motorSpeed = 0;
        motorFore.motorSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (StateManager.GetTmpState().preventGameInput)
        {
            return;
        }
        if (MousePositionValid())
        {

            PidForeArm();
            PidUpperArm();
        }
        else
        {
            motorUpper.motorSpeed = 0;
            motorFore.motorSpeed = 0;
            hingeJointFore.motor = motorFore;
            hingeJointUpper.motor = motorUpper;
        }
    }

    Vector2 GetMousePosition()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
        return mouseWorldPos;
    }

    bool MousePositionValid()
    {
        // Disable arm control if mouse if on top of robot body
        Vector2 mouseWorldPos = GetMousePosition();
        Collider2D[] col = Physics2D.OverlapPointAll(mouseWorldPos);
        //print("Collider length " + col.Length);
        if (col.Length > 0)
        {
            foreach (Collider2D c in col)
            {
                if (c.name == bodyCollider.gameObject.name)
                {
                    return false;
                }
            }
        }
        return true;
    }

    bool ApproxEqual(Vector2 a, Vector2 b)
    {
        if (Mathf.Abs(a.x - b.x) < 0.01 && Mathf.Abs(a.y - b.y) < 0.01)
        {
            return true;
        }
        return false;
    }

    void PidForeArm()
    {
        /*
         * Ensure the fore arm aligns with mouse
         * 
         */

        Vector2 mouseWorldPos = GetMousePosition();
        /*if (!ApproxEqual(currMouseWorldPos, mouseWorldPos))
        {
            print("Mouse has moved " + currMouseWorldPos + "," + mouseWorldPos);
            armDiverging = false;
        }

        if (armDiverging)
        {
            motorFore.motorSpeed = 0;
            hingeJointFore.motor = motorFore;
            return;
        }*/

        Vector2 forearmPos = foreArm.transform.position;
        Vector2 upperarmPos = upperArm.transform.position;

        // This line differs between fore arm and upper arm
        Vector2 expectedDirection = (mouseWorldPos - upperarmPos).normalized;

        Vector2 currentDirection = foreArm.transform.up;
        float deltaAngle = Vector2.SignedAngle(currentDirection, expectedDirection);
        
        float rotateSpeed = -maxRotateSpeedFore * deltaAngle;

        /*foreArmErrorQueue.Enqueue(deltaAngle);
        if (foreArmErrorQueue.Count > 1)
        {
            // This section implements a D component for PID
            float pastError = foreArmErrorQueue.Dequeue();
            float derivative = deltaAngle - pastError;
            if (derivative != 0)
            {
                print("In derivaterm term " + derivative);
                rotateSpeed += foreArmCoeffD * derivative;
            }

            
            //Code below should be safe to delte
            if (Mathf.Abs(deltaAngle) - Mathf.Abs(pastError) > 2)
            {
                print("Setting diverging to true");
                currMouseWorldPos = mouseWorldPos;
                armDiverging = true;
                rotateSpeed = 0;
                foreArmErrorQueue.Clear();
            }
        }*/

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
        print("Delta angle " + deltaAngle);
        float rotateSpeed = -maxRotateSpeedUpper * deltaAngle;
        //if (Mathf.Abs(deltaAngle) < 10) {
        //    rotateSpeed = 0;
        //}

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
