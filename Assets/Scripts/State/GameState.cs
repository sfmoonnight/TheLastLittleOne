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
    public bool tele2 = false;
    public bool tele3 = false;
}
