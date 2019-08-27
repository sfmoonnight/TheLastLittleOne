using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject healthBarUI;
    GameObject energyBarUI;
    GameObject forearm;

    public static GameObject littleOne;

    public static List<Part> parts = new List<Part>();
    public static Part currentPart;
    public static int currentPartIndex;
    

    public float maxHealth;
    public static float health;
    public float maxArmEnergy;
    public static float armEnergy;

    public GameObject energyBarMask;
    public GameObject energyBar;

    public float energyRecovery;
    public float energyBarMaxDistance = 3.62f;

    public static bool recoverEnergy;

    // Start is called before the first frame update
    void Start()
    {
        health = StateManager.GetGameState().maxHealth;
        armEnergy = StateManager.GetGameState().maxArmEnergy;
        healthBarUI = GameObject.Find("HealthBarUI");
        energyBarUI = GameObject.Find("EnergyBarUI");
        forearm = GameObject.Find("forearm");
        littleOne = gameObject;
        /*
        foreach(Part part in forearm.GetComponents<Part>())
        {
            parts.Add(part);
            //print(part.enabled);
        }

        //Repulser rp = forearm.GetComponent<Repulser>();
        */

        //print(parts.Count);
        DeactiveAllParts();
        /*currentPartIndex = 0;
        ActivatePart();*/
        ActivatePart<Repulser>();
        ActivatePart<LaserGun>();

        recoverEnergy = true;
        InvokeRepeating("EnergyRecovery", 0, 0.1f);

    }

    // Update is called once per frame
    void Update()
    {
        //print(armEnergy);
        if (StateManager.GetTmpState().preventGameInput) {
            return;
        }

        energyBarUI.GetComponent<Image>().fillAmount = armEnergy / StateManager.GetGameState().maxArmEnergy;

        healthBarUI.GetComponent<Image>().fillAmount = health / StateManager.GetGameState().maxHealth;
        if(health <= 0)
        {
            StartCoroutine(RestartFromLastCheckPoint(0.5f));
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            /*if(currentPartIndex == 0)
            {
                currentPartIndex = parts.Count - 1;
            }
            else
            {
                currentPartIndex -= 1;
            }

            SwitchWeapon();*/
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            /*if (currentPartIndex == parts.Count - 1)
            {
                currentPartIndex = 0;
            }
            else
            {
                currentPartIndex += 1;
            }

            SwitchWeapon();*/
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //DeactivatePart<LaserGun>();
            DeactivatePart<LightSaber>();
            DeactivatePart<Repulser>();
            ActivatePart<EnergyShield>();

        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ActivatePart<LaserGun>();
            ActivatePart<Repulser>();
            DeactivatePart<EnergyShield>();

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActuatePart<Repulser>();

        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //ActuatePart<LaserGun>();
            DeactivatePart<Repulser>();
            DeactivatePart<EnergyShield>();
            ActivatePart<LightSaber>();

        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ActivatePart<Repulser>();
            DeactivatePart<LightSaber>();
        }

    }

    public void EnergyRecovery()
    {
        if (recoverEnergy)
        {
            if (armEnergy < StateManager.GetGameState().maxArmEnergy)
            {
                armEnergy += energyRecovery;
                Mathf.Clamp(armEnergy, 0, StateManager.GetGameState().maxArmEnergy);
            }

            //energyBar.transform.localScale = energyBarInitScale * (armEnergy / armEnergyMax);
            energyBarMask.transform.localPosition = new Vector3(((StateManager.GetGameState().maxArmEnergy - armEnergy) / StateManager.GetGameState().maxArmEnergy) * energyBarMaxDistance, 0, 0);
        }    
    }

    /*public static void ActivatePart()
    {
        currentPart = parts[currentPartIndex];
        //print(currentPart.partName);
        currentPart.enabled = true;
        currentPart.ActivatePart();
    }*/

    public void UpdateTipColor(Part part)
    {
        GameObject tip = GameObject.Find("Tip");
        tip.GetComponent<SpriteRenderer>().color = part.tipColor;
    }

    public void ActivatePart<T>()
    {
        T inst = forearm.GetComponent<T>();
        Part part = (Part)Convert.ChangeType(inst, typeof(T));
        //part.enabled = true;
        part.ActivatePart();
        UpdateTipColor(part);
    }

    public void DeactivatePart<T>()
    {
        T inst = forearm.GetComponent<T>();
        Part part = (Part)Convert.ChangeType(inst, typeof(T));
        //part.enabled = false;
        part.DeactivatePart();

    }

    /*void ActuatePart()
    {
        currentPart.Actuate();
    }*/

    public void ActuatePart<T>()
    {
        T inst = forearm.GetComponent<T>();
        Part part = (Part)Convert.ChangeType(inst, typeof(T));
        part.Actuate();
        UpdateTipColor(part);
    }

    /*void SwitchWeapon()
    {
        //print("Switch");

        DeactiveAllParts();

        ActivatePart();

        GameObject tip = GameObject.Find("Tip");
        tip.GetComponent<SpriteRenderer>().color = currentPart.tipColor;

        //var type = currentPart.GetType();

        //forearm.AddComponent(type);
    }*/

    /*public static void DeactiveAllParts()
    {
        foreach(Part part in parts)
        {
            part.enabled = false;
            part.DeactivatePart();           
        }

        currentPart = null;
    }*/

    public void DeactiveAllParts()
    {
        foreach (Part part in forearm.GetComponents<Part>())
        {
            part.DeactivatePart();
        }
    }


    IEnumerator RestartFromLastCheckPoint(float time)
    {
        //print("restart");
        GameObject checkPoint = GameObject.Find("checkpoint");
        yield return new WaitForSeconds(time);
        littleOne.transform.position = checkPoint.transform.position;
        health = StateManager.GetGameState().maxHealth;
        armEnergy = StateManager.GetGameState().maxArmEnergy;
        EventManager.TriggerReload();
    }
}
