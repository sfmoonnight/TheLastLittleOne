using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Weapon
{
    public GameObject bullet;
    public GameObject instantiatePoint;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        partName = "lasergun";
        tipColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Actuate()
    {
        if (activated && energyManager.armEnergy > 10)
        {
            Instantiate(bullet, instantiatePoint.transform.position, instantiatePoint.transform.rotation);
            ConsumeEnergy();
        }
    }
}
