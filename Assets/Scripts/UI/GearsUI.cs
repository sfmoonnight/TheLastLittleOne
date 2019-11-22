using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearsUI : MonoBehaviour
{
    StateManager stateManager = Toolbox.GetInstance().GetStateManager();
    public Text gearNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gearNumber.text = "" + stateManager.GetGameState().gears;
    }
}
