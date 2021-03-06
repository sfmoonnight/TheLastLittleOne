﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        //enemy = transform.root.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractionCollider"))
        {
            if (enemy.GetComponent<Enemy>().exist)
            {
                //enemy.GetComponent<Rigidbody2D>().simulated = true;
                enemy.GetComponent<EnemyAI>().Activate();
                //enemy.GetComponent<EnemyAI>().enemyState = EnemyAI.EnemyState.activated;

                //enemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractionCollider"))
        {
            if (enemy.GetComponent<Enemy>().exist)
            {
                //enemy.GetComponent<Rigidbody2D>().simulated = true;
                
                enemy.GetComponent<EnemyAI>().Inactivate();
                //enemy.GetComponent<EnemyAI>().enemyState = EnemyAI.EnemyState.inactivated;
                
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractionCollider"))
        {
            if (enemy.GetComponent<Enemy>().exist)
            {               
                enemy.GetComponent<EnemyAI>().Activate();
            }
        }
    }
}
