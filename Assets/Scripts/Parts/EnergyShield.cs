using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShield : Part
{
    public GameObject shieldSprite;
 
    // Start is called before the first frame update
    void Start()
    {
        partName = "energyshield";
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
            shieldSprite.SetActive(true);
            activated = true;
        }
    }

    public override void DeactivatePart()
    {
        print("deshield");
        shieldSprite.SetActive(false);
        activated = false;
    }
}
