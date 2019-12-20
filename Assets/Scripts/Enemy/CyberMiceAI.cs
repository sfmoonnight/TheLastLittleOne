using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberMiceAI : EnemyAI
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

        if (active)
        {
            Activate();
        }
    }

    public override void Activate()
    {
        base.Activate();
        if (target)
        {
            if (active)
            {
                if (!getHit && !attaking)
                {
                    float direction = target.transform.position.x - self.transform.position.x;
                    self.AddForce(new Vector2(1, 0) * Mathf.Sign(direction) * self.mass * speed * Time.deltaTime);
                    self.drag = 1;
                    //self.velocity += new Vector2(1, 0) * Mathf.Sign(direction) * speed * Time.deltaTime;
                    if (Mathf.Sign(direction) == Mathf.Sign(transform.localScale.x))
                    {
                        Flip();
                    }
                }
                else
                {
                    self.drag = 0;
                }
            }   
        }
    }

    public override void Inactivate()
    {
        base.Inactivate();
        self.velocity = Vector2.zero;
        self.drag = 0;
    }

    public override void Attack()
    {
        base.Attack();
        if (attaking && !getHit)
        {
            //print("attacking");
            float direction = target.transform.position.x - self.transform.position.x;
            Vector2 vector = new Vector2(1 * Mathf.Sign(direction), 1) * self.mass;
            //print(vector);
            self.AddForce(vector * 8, ForceMode2D.Impulse);
        }
    }
}
