using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
    //---Robot
    public float energy;
    public float health;
    public string currentPart;
    //---Resource
    public int gears = 0;

    //---Environment
    public bool teleHub = true;
    public bool tele1 = true;
    public bool tele2a = true;
    public bool tele2b = false;
    public bool tele3 = true;

    public string sceneName;
    public float x;
    public float y;

    //---Upgrade
    public static float maxHealth = 100;
    public int level = 1;
    public static float maxArmEnergy = 100;
}
