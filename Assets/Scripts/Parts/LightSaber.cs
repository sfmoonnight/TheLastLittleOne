using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : Weapon
{
    public GameObject lightSaberSprite;

    // Start is called before the first frame update
    void Start()
    {
        partName = "lightsaber";
        tipColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void ActivatePart()
    {
        if (!activated)
        {
            lightSaberSprite.SetActive(true);
            activated = true;
        }
    }

    public override void DeactivatePart()
    {
        lightSaberSprite.SetActive(false);
        activated = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
           
        }

    }
}
