using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Part> parts;
    public Part currentPart;
    public int currentPartIndex;
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
        }
        currentPartIndex = 0;

        print(parts.Count);
        SwitchPart();
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

            SwitchPart();
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

            SwitchPart();
        }

    }

    void SwitchPart()
    {
        print("Switch");

        foreach(Part part in parts)
        {
            part.enabled = false;
        }

        currentPart = parts[currentPartIndex];

        currentPart.enabled = true;

        GameObject tip = GameObject.Find("Tip");
        tip.GetComponent<SpriteRenderer>().color = currentPart.tipColor;

        //var type = currentPart.GetType();

        //forearm.AddComponent(type);
    }
}
