using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float deanthAnimTime;
    public int gearDrop;
    public Vector2 spawningpoint;

    public Rigidbody2D self;
    public bool exist;

    GameObject upperArm;
    HingeJoint2D upperArmJoint;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = maxHealth;
        spawningpoint = transform.position;
        upperArm = GameObject.Find("UpperArm");
        upperArmJoint = upperArm.GetComponent<HingeJoint2D>();

        if (GetComponent<Rigidbody2D>())
        {
            self = GetComponent<Rigidbody2D>();
        }

        //print("Subscribint");
        //---Set up event
        EventManager.OnReload += Reload;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            float dam = collision.GetComponent<Weapon>().damage;
        }
        if (collision.CompareTag("SpeedWeapon"))
        {
            print("inspeedweapon");
            float speed = Mathf.Abs(upperArmJoint.motor.motorSpeed);
            float dam = collision.GetComponent<Weapon>().damage * speed * 0.01f + collision.GetComponent<Weapon>().damage;
            TakeDamage(dam);
            //print(speed);
            //print(dam);
        }
    }


    //----------Functions
    //---Death functions
    public virtual void DestroyEnemy()
    {
        InactivateAllColliders();
        StartCoroutine(HideEnemy(deanthAnimTime));
        self.bodyType = RigidbodyType2D.Static;
        exist = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    public virtual void PlayDeathAnimation()
    {

    }

    public virtual void DropGears()
    {
        StateManager.ChangeGearNumber(gearDrop);
    }

    public void InactivateAllColliders()
    {
        Collider2D[] colliders = gameObject.GetComponents<Collider2D>();
        Collider2D[] childColliders = gameObject.GetComponentsInChildren<Collider2D>();
        foreach(Collider2D c in colliders)
        {
            c.enabled = false;
        }

        foreach (Collider2D c in childColliders)
        {
            c.enabled = false;
        }
    }

    //---Reload functions
    public virtual void Reload()
    {
        // repositioning code
        Reposition();
        health = maxHealth;
        ResetRigidbody();
        EnableSpriteRenderer();
        EnableAllColliders();
        //ResetRigidbody();
        exist = true;
    }

    public virtual void Reposition()
    {
        //print("relocate");
        transform.position = spawningpoint;
    }

    public void EnableSpriteRenderer()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void EnableAllColliders()
    {
        Collider2D[] colliders = gameObject.GetComponents<Collider2D>();
        Collider2D[] childColliders = gameObject.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D c in colliders)
        {
            c.enabled = true;
        }
        foreach (Collider2D c in childColliders)
        {
            c.enabled = true;
        }
    }

    public void ResetRigidbody()
    {
        self.bodyType = RigidbodyType2D.Dynamic;
    }

    //----------Coroutines
    IEnumerator HideEnemy(float time)
    {
        PlayDeathAnimation();
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        DropGears();
    }
}
