using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    public string name;
    public Color tipColor;

    public float armEnergyMax;
    public float armEnergy;
    public float energyCost;

    public float energyRecovery;
    public GameObject energyBarMask;
    public GameObject energyBar;
    public Vector3 energyBarInitScale;
    public float energyBarMaxDistance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void EnergyRecovery()
    {
        if(armEnergy < armEnergyMax)
        {
            armEnergy += energyRecovery;
            Mathf.Clamp(armEnergy, 0, armEnergyMax);
        }

        //energyBar.transform.localScale = energyBarInitScale * (armEnergy / armEnergyMax);
        energyBarMask.transform.localPosition = new Vector3(((armEnergyMax - armEnergy) / armEnergyMax) * energyBarMaxDistance, 0, 0);
    }
}
