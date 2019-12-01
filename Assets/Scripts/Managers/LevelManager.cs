using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameManager gameManager;
    StateManager stateManager;
    EnergyManager energyManager;

    GameObject littleOne;
    GameObject healthBarUI;
    GameObject energyBarUI;
    GameObject forearm;

    GameObject armEnergyBarMask;
    void Awake()
    {
        littleOne = GameObject.Find("body");
        healthBarUI = GameObject.Find("HealthBarUI");
        forearm = GameObject.Find("forearm");

        energyBarUI = GameObject.Find("EnergyBarUI");
        armEnergyBarMask = GameObject.Find("ArmEnergyBarMask");
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = Toolbox.GetInstance().GetGameManager();
        //stateManager = Toolbox.GetInstance().GetStateManager();
        energyManager = Toolbox.GetInstance().GetEnergyManager();

        gameManager.SetUpGame(littleOne, forearm, healthBarUI);
        energyManager.SetUpEnergy(energyBarUI, armEnergyBarMask);
        stateManager.SetUpState(littleOne);
        SetUpScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUpScene()
    {
        print(gameManager.forearm);
        print(gameManager.GetCurrentPart());
        gameManager.DeactiveAllParts();
        gameManager.ActivatePart<Repulser>();
        //gameManager.SwitchPart(gameManager.currentPart.partName);

    }
}
