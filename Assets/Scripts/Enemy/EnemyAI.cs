using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    /*public enum EnemyState
    {
        activated,
        inactivated,
        hit,
        attack
    }

    public EnemyState enemyState;
    */

    public GameObject target;
    public Rigidbody2D self;
    public float speed;
    public float attackRate;
    public GameObject playerDetection;

    public bool active;
    public bool attaking;
    public bool getHit;

    // Start is called before the first frame update
    public virtual void Start()
    {
        active = false;
        attaking = false;
        getHit = false;
        //enemyState = EnemyState.inactivated;
        self = GetComponent<Rigidbody2D>();

        EventManager.OnReload += Reload;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        /*if (enemyState == EnemyState.inactivated)
        {
            Inactivate();
        }

        if (enemyState == EnemyState.activated)
        {
            Activate();
        }

        if (enemyState == EnemyState.hit)
        {
            Hit();
        }

        if (enemyState == EnemyState.attack)
        {
            Attack();
        }*/
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }


    public virtual void Activate()
    {
        active = true;
        target = GameObject.Find("body");
    }

    public virtual void Inactivate()
    {
        active = false;
        target = null;
    }

    public virtual void StartAttack()
    {
        if (!getHit)
        {
            InvokeRepeating("Attack", 0, attackRate);
        }
    }

    public virtual void StopAttack()
    {
        attaking = false;
        CancelInvoke("Attack");
    }

    public virtual void Attack()
    {
        attaking = true;
    }

    public virtual void Hit()
    {
        getHit = true;
        StartCoroutine(GetHit(0.3f));
    }

    public virtual void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }

    public virtual void Reload()
    {
        //print("inactivate");
        //enemyState = EnemyState.inactivated;
        active = false;
        attaking = false;
        getHit = false;
    }

    IEnumerator GetHit(float time)
    {
        //---If the player exit the detection zone in 0.3s, will it still be activated?
        //GameObject interactionCollider = GameObject.Find("InteractionCollider");
        //Physics2D.IgnoreCollision(playerDetection.GetComponent<Collider2D>(), interactionCollider.GetComponent<Collider2D>(), true);
        //playerDetection.GetComponent<Collider2D>().enabled = false;
        active = false;
        attaking = false;
        yield return new WaitForSeconds(time);
        getHit = false;
        //enemyState = EnemyState.activated;
        //Physics2D.IgnoreCollision(playerDetection.GetComponent<Collider2D>(), interactionCollider.GetComponent<Collider2D>(), false);
        //playerDetection.GetComponent<Collider2D>().enabled = true;
    }
}
