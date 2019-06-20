using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<Part> parts = new List<Part>();
    public static Part currentPart;
    public static int currentPartIndex;
    GameObject forearm;

    public float armEnergyMax;
    public float armEnergy;

    // Start is called before the first frame update
    void Start()
    {
        forearm = GameObject.Find("forearm");
        foreach(Part part in forearm.GetComponents<Part>())
        {
            parts.Add(part);
            print(part.enabled);
        }

        print(parts.Count);
        DeactiveAllParts();
        currentPartIndex = 0;
        ActivatePart();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(currentPartIndex == 0)
            {
                currentPartIndex = parts.Count - 1;
            }
            else
            {
                currentPartIndex -= 1;
            }

            SwitchWeapon();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentPartIndex == parts.Count - 1)
            {
                currentPartIndex = 0;
            }
            else
            {
                currentPartIndex += 1;
            }

            SwitchWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            currentPartIndex = 2;
            SwitchWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentPartIndex = 0;
            SwitchWeapon();
            ActuatePart();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentPartIndex = 1;
            SwitchWeapon();
            ActuatePart();
        }

    }

    public static void ActivatePart()
    {
        currentPart = parts[currentPartIndex];
        //print(currentPart.partName);
        currentPart.enabled = true;
        currentPart.ActivatePart();
    }

    void ActuatePart()
    {
        currentPart.Actuate();
    }

    void SwitchWeapon()
    {
        print("Switch");

        DeactiveAllParts();

        ActivatePart();

        GameObject tip = GameObject.Find("Tip");
        tip.GetComponent<SpriteRenderer>().color = currentPart.tipColor;

        //var type = currentPart.GetType();

        //forearm.AddComponent(type);
    }

    public static void DeactiveAllParts()
    {
        foreach(Part part in parts)
        {
            part.enabled = false;
            part.DeactivatePart();
        }

        currentPart = null;
    }
}
