using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage;
    public float hitForce = 0;
    public Vector2 hitDirection = Vector2.zero;

    GameObject upperArm;
    public HingeJoint2D upperArmJoint;
    // Start is called before the first frame update
    public virtual void Start()
    {
        upperArm = GameObject.Find("UpperArm");
        upperArmJoint = upperArm.GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InteractionCollider"))
        {
            print(damage);
            GameManager.health -= damage;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {

    }

    public virtual void OnTriggerStay2D(Collider2D other)
    {

    }

}
