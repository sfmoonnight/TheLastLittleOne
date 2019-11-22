using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : Weapon
{
    public GameObject lightSaberSprite;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        partName = "lightsaber";
        tipColor = Color.white;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(energyManager.armEnergy <= 0)
        {
            DeactivatePart();
        }
    }
    public override void ActivatePart()
    {
        if(energyManager.armEnergy > 10)
        {
            base.ActivatePart();
            lightSaberSprite.SetActive(true);
            InvokeRepeating("ConsumeEnergy", 0, 0.1f);
            energyManager.recoverEnergy = false;
        }
    }

    public override void DeactivatePart()
    {
        base.DeactivatePart();
        lightSaberSprite.SetActive(false);
        CancelInvoke("ConsumeEnergy");
        energyManager.recoverEnergy = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
           
        }

    }
}
