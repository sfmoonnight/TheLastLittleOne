using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    GameObject energyBarUI;
    public float armEnergy;

    GameObject armEnergyBarMask;
    GameObject armEnergyBar;

    public float energyRecovery = 1;
    public float energyBarMaxDistance = 3.62f;

    public bool recoverEnergy;

    StateManager stateManager;
    // Start is called before the first frame update
    void Start()
    {
        stateManager = Toolbox.GetInstance().GetStateManager();
        armEnergy = stateManager.GetGameState().maxArmEnergy;
        energyBarUI = GameObject.Find("EnergyBarUI");
        armEnergyBar = GameObject.Find("ArmEnergyBar");
        armEnergyBarMask = GameObject.Find("ArmEnergyBarMask");

        recoverEnergy = true;

        InvokeRepeating("EnergyRecovery", 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        energyBarUI.GetComponent<Image>().fillAmount = armEnergy / stateManager.GetGameState().maxArmEnergy;
        
    }

    public void EnergyRecovery()
    {
        if (recoverEnergy)
        {
            if (armEnergy < stateManager.GetGameState().maxArmEnergy)
            {
                armEnergy += energyRecovery;
                Mathf.Clamp(armEnergy, 0, stateManager.GetGameState().maxArmEnergy);
            }

            //energyBar.transform.localScale = energyBarInitScale * (armEnergy / armEnergyMax);
            armEnergyBarMask.transform.localPosition = new Vector3(((stateManager.GetGameState().maxArmEnergy - armEnergy) / stateManager.GetGameState().maxArmEnergy) * energyBarMaxDistance, 0, 0);
        }
    }

    public void ResetEnergy()
    {
        armEnergy = stateManager.GetGameState().maxArmEnergy;
    }
}
