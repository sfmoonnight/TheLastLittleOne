using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningPoint : MonoBehaviour
{
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
        print("Go456left");
        if (gameObject.CompareTag("GoLeft"))
        {
            print("Goleft");
            collision.GetComponent<MovingPlatform>().direction = MovingPlatform.Direction.left;
        }
        if (gameObject.CompareTag("GoRight"))
        {
            collision.GetComponent<MovingPlatform>().direction = MovingPlatform.Direction.right;
        }
    }
}
