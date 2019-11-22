using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void ReloadAction();
    public static event ReloadAction OnReload;

    public delegate void RemoveListeners();
    public static event RemoveListeners OnRemove;

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

    public static void TriggerRemove()
    {
        print("RemoveListeners");
        if (OnRemove != null)
        {
            print("OnRemove not null");
            OnRemove();
        }
        else
        {
            print("OnRemove is null");
        }
    }
}
