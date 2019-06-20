﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Part
{
    public GameObject bullet;
    public GameObject instantiatePoint;

    // Start is called before the first frame update
    void Start()
    {
        partName = "lasergun";
        tipColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Actuate()
    {
        Instantiate(bullet, instantiatePoint.transform.position, instantiatePoint.transform.rotation);
    }
}
