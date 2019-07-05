using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void ReloadAction();
    public static event ReloadAction OnReload;

    public static void TriggerReload()
    {
        print("triggering reload");
        if (OnReload != null)
        {
            print("OnReload not null");
            OnReload();
        }
        else
        {
            print("OnReload is null");
        }
    }
}
