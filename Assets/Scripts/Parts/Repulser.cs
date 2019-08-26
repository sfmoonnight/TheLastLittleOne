using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Repulser : Part
{
    public float force;
    public GameObject tip;


    //GameObject forearm;
    Rigidbody2D body;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //forearm = GameObject.Find("forearm");
        partName = "repulser";
        tipColor = Color.yellow;
        body = GameObject.Find("body").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Actuate()
    {
        if (activated)
        {
            //GameManager.DeactiveAllParts();
            if (GameManager.armEnergy >= energyCost)
            {
                body.AddForce(-transform.up * force, ForceMode2D.Impulse);
                ConsumeEnergy();
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
