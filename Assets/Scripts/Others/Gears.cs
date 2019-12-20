using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gears : MonoBehaviour
{
    StateManager stateManager;
    // Start is called before the first frame update
    void Start()
    {
        stateManager = Toolbox.GetInstance().GetStateManager();

        float x = Random.Range(-1f, 1f);
        float y = Random.Range(0f, 1f);

        GetComponent<Rigidbody2D>().AddForce(new Vector2(x,y)*10, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractionCollider"))
        {
            print("collect");
            stateManager.ChangeGearNumber(10);
            Destroy(this.gameObject);
        }
    }
    
}
