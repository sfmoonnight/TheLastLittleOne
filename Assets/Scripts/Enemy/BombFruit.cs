using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFruit : Enemy
{
    
    Animator anim;
   
    // Start is called before the first frame update
    protected override void Start()
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

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        //print("detectplayer");
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Action());
        }

        //DestroyEnemy(1.5f);
    }


    //-------Functions
    public override void DealDamage()
    {
        
    }

    public void AnimExplode()
    {
        //print("death triggered on fruit");
       /* try
        {
            // https://answers.unity.com/questions/1403162/how-to-check-if-animator-is-playing.html
            // string name = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            AnimatorClipInfo aci = anim.GetCurrentAnimatorClipInfo(0)[0];
        }
        catch (System.IndexOutOfRangeException e)
        {
            anim.SetTrigger("explode");
            self.bodyType = RigidbodyType2D.Static;
        }*/

        if (anim.GetCurrentAnimatorClipInfoCount(0) == 0)
        {
            anim.SetTrigger("explode");
            self.bodyType = RigidbodyType2D.Static;
        }
        
        //self.simulated = false;
    }

    public override void PlayDeathAnimation()
    {
        AnimExplode();
    }

    //-------Coroutines

    public IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            print("WaitAndPrint " + Time.time);
        }
    }

    public override void Reload()
    {
        base.Reload();
        self.bodyType = RigidbodyType2D.Static;
        // custom stuffz
        //print("Event triggered on fruit");
    }

    IEnumerator Action()
    {
        AnimExplode();
        DestroyEnemy(animTime);
        //print("Going to wait for " + animTime);
        yield return new WaitForSeconds(animTime); 
        //print("I'm here");
        GameManager.RestartFromLastCheckPoint();
        yield return null;
    }
}
