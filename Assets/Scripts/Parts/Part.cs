using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    public string partName;
    public Color tipColor;

    public float energyCost;
    

    public bool activated;

    public EnergyManager energyManager;

    public virtual void Awake()
    {
        energyManager = Toolbox.GetInstance().GetEnergyManager();
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public virtual void ActivatePart()
    {
        enabled = true;
        if (!activated)
        {
            activated = true;
        }
    }

    public virtual void DeactivatePart()
    {
        enabled = false;
        if (activated)
        {         
            activated = false;
        }
    }

    public virtual void Actuate()
    {

    }

    public virtual void PlayAnimation()
    {

    }

    public virtual void ConsumeEnergy()
    {
        //print(energyManager);
        if(energyManager.armEnergy > energyCost)
        {
            energyManager.armEnergy -= energyCost;
        }  
    }
}
