using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    GameObject healthBarUI;
    public GameObject forearm;
    
    public GameObject littleOne;

    public static List<Part> parts = new List<Part>();
    
    public static int currentPartIndex;

    public Part currentPart;
    
    public float maxHealth;
    public static float health;
    public float maxArmEnergy;

    public StateManager stateManager;
    EnergyManager energyManager;

    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //print("gamemanager=====================================");
        healthBarUI = GameObject.Find("HealthBarUI");

        stateManager = Toolbox.GetInstance().GetStateManager();
        energyManager = Toolbox.GetInstance().GetEnergyManager();

        health = GameState.maxHealth;
        //forearm = GameObject.Find("forearm");
        //print("***forearm: " + forearm);
        
        //print("****current part: " + currentPart);
        
        //littleOne = GameObject.Find("body");

        //SwitchPart(currentPart.partName);
        /*
        foreach(Part part in forearm.GetComponents<Part>())
        {
            parts.Add(part);
            //print(part.enabled);
        }

        //Repulser rp = forearm.GetComponent<Repulser>();
        */

        //print(parts.Count);
        //DeactiveAllParts();
        /*currentPartIndex = 0;
        ActivatePart();*/
        //ActivatePart<Repulser>();
        //ActivatePart<LaserGun>();

        
    }

    // Update is called once per frame
    void Update()
    {
        healthBarUI.GetComponent<Image>().fillAmount = health / GameState.maxHealth;
        //print(armEnergy);
        if (stateManager.GetTmpState().preventGameInput) {
            return;
        }

        
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
            DeactivatePart<LaserGun>();
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
            DeactivatePart<Repulser>();
            DeactivatePart<EnergyShield>();
            print("+++++" + currentPart.partName);
            if (currentPart.partName == "lightsaber")
            {
                ActivatePart<LightSaber>();
            }else if(currentPart.partName == "lasergun")
            {
                print("-----activate");
                ActivatePart<LaserGun>();
                ActuatePart<LaserGun>();
            }

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

    public void SetUpGame(GameObject body, GameObject fArm, GameObject hUI)
    {
        littleOne = body;
        forearm = fArm;
        healthBarUI = hUI;
        currentPart = forearm.GetComponent<LaserGun>();
    }

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

    public Part GetCurrentPart()
    {
        return currentPart;
    }

    public void SetCurrentPart()
    {
        currentPart = forearm.GetComponent<LaserGun>();
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

    public void SwitchPart(String pname)
    {
        if(pname == "lasergun")
        {
            currentPart = forearm.GetComponent<LaserGun>();
        }
        if (pname == "lightsaber")
        {
            currentPart = forearm.GetComponent<LightSaber>();
        }
    }


    IEnumerator RestartFromLastCheckPoint(float time)
    {
        //print("restart");
        GameObject checkPoint = GameObject.Find("checkpoint");
        yield return new WaitForSeconds(time);
        littleOne.transform.position = checkPoint.transform.position;
        //print(checkPoint.transform.position);
        //print(littleOne.transform.position);


        health = GameState.maxHealth;
        energyManager.ResetEnergy();
        //stateManager.SaveState();
        EventManager.TriggerReload();
    }

    
}
