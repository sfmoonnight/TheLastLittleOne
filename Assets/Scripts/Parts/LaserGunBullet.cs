using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunBullet : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

}
