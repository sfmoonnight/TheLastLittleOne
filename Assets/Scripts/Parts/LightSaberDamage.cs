using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaberDamage : Damage
{
   
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyHitbox"))
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();

            if (enemy.health > 0)
            {
                float speed = Mathf.Abs(upperArmJoint.motor.motorSpeed);
                float dam = damage * speed * 0.01f + damage;
                enemy.TakeDamage(dam);
                //print(speed);
                //print(dam);
                hitDirection = enemy.transform.position - transform.position;
                enemy.GetComponent<EnemyAI>().Hit();
                enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                enemy.GetComponent<Rigidbody2D>().AddForce(hitDirection.normalized * hitForce, ForceMode2D.Impulse);
                print(hitForce);
            }
           
        }
        
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("EnemyHitbox"))
        {
            
        }
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("EnemyHitbox"))
        {

        }
    }
}
