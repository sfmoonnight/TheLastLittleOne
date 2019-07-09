using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Repulser : Part
{
    public float force;
    public GameObject tip;

    GameObject energyBarUI;

    //GameObject forearm;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        energyBarUI = GameObject.Find("EnergyBarUI");
        //forearm = GameObject.Find("forearm");
        partName = "repulser";
        tipColor = Color.yellow;
        body = GameObject.Find("body").GetComponent<Rigidbody2D>();

        armEnergy = armEnergyMax;
        InvokeRepeating("EnergyRecovery", 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        energyBarUI.GetComponent<Image>().fillAmount = armEnergy/armEnergyMax;
    }

    public override void Actuate()
    {
        if (activated)
        {
            //GameManager.DeactiveAllParts();
            if (armEnergy >= energyCost)
            {
                body.AddForce(-transform.up * force, ForceMode2D.Impulse);
                armEnergy -= energyCost;
                PlayAnimation();
            }
            //GameManager.ActivatePart();
        }
    }

    public override void PlayAnimation()
    {
        Animator ani = tip.GetComponent<Animator>();
        ani.SetTrigger("repulse");
    }
}
