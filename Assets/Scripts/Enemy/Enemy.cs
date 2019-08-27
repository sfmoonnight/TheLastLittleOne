using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float deanthAnimTime;
    public int gearDrop;
    public Vector2 spawningpoint;

    public Rigidbody2D self;
    public bool exist;

    public GameObject healthBarImage;

    GameObject upperArm;
    Vector2 startingScale;
    

    // Start is called before the first frame update
    public virtual void Start()
    {
        health = maxHealth;
        spawningpoint = transform.position;
        startingScale = transform.localScale;
        upperArm = GameObject.Find("UpperArm");

        if (healthBarImage)
        {
            healthBarImage.GetComponent<Image>().enabled = false;
        }

        if (GetComponent<Rigidbody2D>())
        {
            self = GetComponent<Rigidbody2D>();
        }

        //print("Subscribint");
        //---Set up event
        EventManager.OnReload += Reload;
        EventManager.OnRemove += RemoveListeners;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (healthBarImage)
        {
            healthBarImage.GetComponent<Image>().fillAmount = health / maxHealth;
            if (health < maxHealth && health > 0)
            {
                healthBarImage.GetComponent<Image>().enabled = true;
            }
            else
            {
                healthBarImage.GetComponent<Image>().enabled = false;
            }
        }  
    }

    //----------Functions
    //---Death functions
    public virtual void DestroyEnemy()
    {
        print("destroy");
        InactivateAllColliders();
        StartCoroutine(HideEnemy(deanthAnimTime));
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

    public void HideSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        if (gameObject.GetComponent<SpriteMask>())
        {
            gameObject.GetComponent<SpriteMask>().enabled = false;
        }
    }

    //---Reload functions
    public virtual void Reload()
    {
        // repositioning code
        EnableSpriteRenderer();
        Reposition();
        health = maxHealth;
        ResetRigidbody();   
        EnableAllColliders();
        exist = true;
    }

    public virtual void RemoveListeners()
    {
        print("removelisteners");
        EventManager.OnReload -= Reload;
        EventManager.OnRemove -= RemoveListeners;
    }

    public virtual void Reposition()
    {
        //print("relocate");
        transform.position = spawningpoint;
        transform.rotation = Quaternion.identity;
        transform.localScale = startingScale;
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

    public virtual void ResetRigidbody()
    {
        self.bodyType = RigidbodyType2D.Dynamic;
    }


    //----------Coroutines
    IEnumerator HideEnemy(float time)
    {
        PlayDeathAnimation();
        yield return new WaitForSeconds(time);
        HideSprite();
        self.bodyType = RigidbodyType2D.Static;
        DropGears();
    }
}
