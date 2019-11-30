using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Assembly : MonoBehaviour
{
    public TMP_Dropdown options;
    public Canvas menu;
    string interactionColliderTag = "InteractionCollider";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(interactionColliderTag))
        {
            menu.GetComponent<CanvasGroup>().alpha = 1;
            menu.GetComponent<CanvasGroup>().interactable = true;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(interactionColliderTag))
        {    
            menu.GetComponent<CanvasGroup>().alpha = 0;
            menu.GetComponent<CanvasGroup>().interactable = false;
        }
    }

    public void SwitchWeapon()
    {
        if(options.value == 0)
        {
            Toolbox.GetInstance().GetGameManager().SwitchPart("lasergun");
        }
        else if(options.value == 1)
        {
            Toolbox.GetInstance().GetGameManager().SwitchPart("lightsaber");
        }
    }


}
