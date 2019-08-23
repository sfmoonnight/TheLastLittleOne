using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    public string partName;
    public Color tipColor;

    public float armEnergy;

    public float energyCost;

    public float energyRecovery;
    public GameObject energyBarMask;
    public GameObject energyBar;
    public Vector3 energyBarInitScale;
    public float energyBarMaxDistance;

    public bool activated;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnergyRecovery()
    {
        if(armEnergy < StateManager.GetGameState().maxArmEnergy)
        {
            armEnergy += energyRecovery;
            Mathf.Clamp(armEnergy, 0, StateManager.GetGameState().maxArmEnergy);
        }

        //energyBar.transform.localScale = energyBarInitScale * (armEnergy / armEnergyMax);
        energyBarMask.transform.localPosition = new Vector3(((StateManager.GetGameState().maxArmEnergy - armEnergy) / StateManager.GetGameState().maxArmEnergy) * energyBarMaxDistance, 0, 0);
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
}
