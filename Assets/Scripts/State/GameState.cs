using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public int level = 1;
    public int money = 0;

    public bool teleHub = true;
    public bool tele1 = true;
    public bool tele2a = true;
    public bool tele2b = true;
    public bool tele3 = false;

    public string sceneName;
    public int x;
    public int y;
}
