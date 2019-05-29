using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repulser : Part
{
    public float force;

    //GameObject forearm;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        //forearm = GameObject.Find("forearm");
        tipColor = Color.yellow;
        body = GameObject.Find("body").GetComponent<Rigidbody2D>();

        armEnergy = armEnergyMax;
        InvokeRepeating("EnergyRecovery", 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Repulse();
        }
    }

    void Repulse()
    {
        if(armEnergy >= energyCost)
        {
            body.AddForce(-transform.up * force, ForceMode2D.Impulse);
            armEnergy -= energyCost;
        }
    }
}
