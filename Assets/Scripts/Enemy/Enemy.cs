using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage;
    public float maxHealth;
    public float health;
    public float animTime;

    public Rigidbody2D self;
    public bool exist;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        self = GetComponent<Rigidbody2D>();

        //print("Subscribint");
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
            TakeDamage(dam);
        }
    }


    //----------Functions
    public virtual void DestroyEnemy(float time)
    {
        StartCoroutine(HideEnemy(time));
        exist = false;
    }

    public virtual void DealDamage()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            PlayDeathAnimation();
            DestroyEnemy(animTime);
        }
    }

    public virtual void PlayDeathAnimation()
    {

    }

    public void InactivateAllColliders()
    {
        Collider2D[] colliders = gameObject.GetComponents<Collider2D>();
        foreach(Collider2D c in colliders)
        {
            c.enabled = false;
        }
    }

    public void Reposition()
    {
        //print("relocate");
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void EnableSpriteRenderer()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void EnableAllColliders()
    {
        Collider2D[] colliders = gameObject.GetComponents<Collider2D>();
        foreach (Collider2D c in colliders)
        {
            c.enabled = true;
        }
    }

    public void ResetRigidbody()
    {
        self.bodyType = RigidbodyType2D.Dynamic;
    }

    public virtual void Reload()
    {
        // repositioning code
        Reposition();
        EnableSpriteRenderer();
        EnableAllColliders();
        //ResetRigidbody();
        exist = true;
    }


    //----------Coroutines
    IEnumerator HideEnemy(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        InactivateAllColliders();
    }
}
