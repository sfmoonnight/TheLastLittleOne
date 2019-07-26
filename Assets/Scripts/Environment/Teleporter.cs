using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{
    public GameObject worldMapCanvas;
    bool near = false;
    bool activated = false;
    string interactionColliderTag = "InteractionCollider";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.E))
         {
            if (near)
            {
                Toggle();
            }
         }
    }

    void Toggle()
    {
        if (activated)
        {
            Deactivate();
        }
        else
        {
            Activate();
        }
    }

    bool EnableButton(string goName)
    {
        Button btn = GameObject.Find(goName).GetComponent<Button>();
        btn.interactable = true;
        return btn.interactable;
    }

    bool DisableButton(string goName)
    {
        Button btn = GameObject.Find(goName).GetComponent<Button>();
        btn.interactable = false;
        return btn.interactable;
    }

    void Refresh()
    {
        GameState gs = StateManager.getGameState();

        bool a;
        a = gs.teleHub ? EnableButton("teleHub") : DisableButton("teleHub");
        a = gs.tele1 ? EnableButton("tele1") : DisableButton("tele1");
        a = gs.tele2 ? EnableButton("tele2") : DisableButton("tele2");
        a = gs.tele3 ? EnableButton("tele3") : DisableButton("tele3");
    }

    void Activate()
    {
        Refresh();
        CanvasGroup cg = worldMapCanvas.GetComponent<CanvasGroup>();
        cg.alpha = 1;
        cg.blocksRaycasts = true;
        cg.interactable = true;
        activated = true;
        StateManager.getTmpState().preventGameInput = true;
    }

    void Deactivate()
    {
        CanvasGroup cg = worldMapCanvas.GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.blocksRaycasts = false;
        cg.interactable = false;
        activated = false;
        StateManager.getTmpState().preventGameInput = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(interactionColliderTag)) {
            near = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(interactionColliderTag)) {
            near = false;
            Deactivate();
        }
    }



}
