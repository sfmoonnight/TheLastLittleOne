using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
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
                if(enemy.GetComponent<EnemyAI>().active && !enemy.GetComponent<EnemyAI>().getHit)
                {
                    enemy.GetComponent<EnemyAI>().StartAttack();
                }  
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractionCollider"))
        {
            if (enemy.GetComponent<Enemy>().exist)
            {
                enemy.GetComponent<EnemyAI>().StopAttack();
                //enemy.GetComponent<EnemyAI>().attaking = false;
            }
        }
    }
}
