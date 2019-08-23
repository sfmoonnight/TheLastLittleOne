﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject healthBarUI;
    GameObject forearm;

    public static GameObject littleOne;

    public static List<Part> parts = new List<Part>();
    public static Part currentPart;
    public static int currentPartIndex;
    

    public float maxHealth;
    public static float health;
    public float armEnergyMax;
    public float armEnergy;

    // Start is called before the first frame update
    void Start()
    {
        health = StateManager.GetGameState().maxHealth;
        healthBarUI = GameObject.Find("HealthBarUI");
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
    }

    // Update is called once per frame
    void Update()
    {

        if (StateManager.GetTmpState().preventGameInput) {
            return;
        }

        healthBarUI.GetComponent<Image>().fillAmount = health / StateManager.GetGameState().maxHealth;
        if(health <= 0)
        {
            RestartFromLastCheckPoint();
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
            DeactivatePart<LaserGun>();
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


    public static void RestartFromLastCheckPoint()
    {
        //print("restart");
        GameObject checkPoint = GameObject.Find("checkpoint");
        littleOne.transform.position = checkPoint.transform.position;
        health = StateManager.GetGameState().maxHealth;
        EventManager.TriggerReload();
    }
}
