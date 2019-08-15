using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public int level = 1;
    public int gears = 0;

    public bool teleHub = true;
    public bool tele1 = true;
    public bool tele2a = true;
    public bool tele2b = false;
    public bool tele3 = true;

    public string sceneName;
    public int x;
    public int y;
}
