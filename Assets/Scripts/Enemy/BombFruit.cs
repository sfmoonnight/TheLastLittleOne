using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFruit : Enemy
{
    Rigidbody2D self;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        health = 0;
        
        self = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        self.Sleep();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("detectplayer");
        if (collision.CompareTag("Player"))
        {
            Explode();
        }

        //DestroyEnemy(1.5f);
    }

    void Explode()
    {
        
        anim.SetTrigger("explode");
        self.bodyType = RigidbodyType2D.Static;
        DestroyEnemy(0.5f);
    }
}
