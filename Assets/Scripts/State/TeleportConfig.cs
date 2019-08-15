using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportConfig
{
    // Dictionary that maps teleporter name to scene name
    public Dictionary<string, string> mapping = new Dictionary<string, string>();
    
    public TeleportConfig()
    {
        mapping.Add("teleHub", "Hub");
        mapping.Add("tele1", "Level1");
        mapping.Add("tele2a", "Level2");
        mapping.Add("tele2b", "Level2");
        mapping.Add("tele3", "TestScene");
    }
}
