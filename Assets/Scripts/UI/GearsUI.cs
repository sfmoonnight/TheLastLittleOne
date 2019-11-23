using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearsUI : MonoBehaviour
{
    StateManager stateManager;
    public Text gearNumber;
    // Start is called before the first frame update
    void Start()
    {
        stateManager = Toolbox.GetInstance().GetStateManager();
    }

    // Update is called once per frame
    void Update()
    {
        gearNumber.text = "" + stateManager.GetGameState().gears;
        //print(stateManager);

    }
}
