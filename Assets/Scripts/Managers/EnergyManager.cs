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
        armEnergy = GameState.maxArmEnergy;
        //energyBarUI = GameObject.Find("EnergyBarUI");
        //print(energyBarUI);
        armEnergyBar = GameObject.Find("ArmEnergyBar");
        armEnergyBarMask = GameObject.Find("ArmEnergyBarMask");

        recoverEnergy = true;

        InvokeRepeating("EnergyRecovery", 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        energyBarUI.GetComponent<Image>().fillAmount = armEnergy / GameState.maxArmEnergy;
        //print(energyBarUI);
        //print(stateManager);
    }

    public void SetUpEnergy(GameObject eUI, GameObject eMask)
    {
        energyBarUI = eUI;
        armEnergyBarMask = eMask;
    }

    public void EnergyRecovery()
    {
        if (recoverEnergy)
        {
            if (armEnergy < GameState.maxArmEnergy)
            {
                armEnergy += energyRecovery;
                Mathf.Clamp(armEnergy, 0, GameState.maxArmEnergy);
            }

            //energyBar.transform.localScale = energyBarInitScale * (armEnergy / armEnergyMax);
            //print(stateManager);
            //print(armEnergyBarMask);
            armEnergyBarMask.transform.localPosition = new Vector3(((GameState.maxArmEnergy - armEnergy) / GameState.maxArmEnergy) * energyBarMaxDistance, 0, 0);
        }
    }

    public void ResetEnergy()
    {
        armEnergy = GameState.maxArmEnergy;
    }
}
