using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFruit : Enemy
{
    
    Animator anim;
   
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();      
        
        anim = GetComponent<Animator>();
        
        //self.Sleep();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    //-------Functions
    public override void ResetRigidbody()
    {
        self.bodyType = RigidbodyType2D.Static;
    }

    public override void PlayDeathAnimation()
    {
        base.PlayDeathAnimation();
        if (anim.GetCurrentAnimatorClipInfoCount(0) == 0)
        {
            anim.SetTrigger("explode");        
        }
        //AnimExplode();
    }

    public override void DestroyEnemy()
    {
        base.DestroyEnemy();
        self.velocity = Vector2.zero;
    }

    //-------Coroutines

    /*public IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            print("WaitAndPrint " + Time.time);
        }
    }*/

    public override void Reload()
    {
        base.Reload();
        // custom stuffz
        self.bodyType = RigidbodyType2D.Static;
        //print("Event triggered on fruit");
    }

    /*IEnumerator Action()
    {
        DestroyEnemy(animTime);
        //print("Going to wait for " + animTime);
        yield return new WaitForSeconds(animTime); 
        //print("I'm here");
        GameManager.RestartFromLastCheckPoint();
        yield return null;
    }*/
}
