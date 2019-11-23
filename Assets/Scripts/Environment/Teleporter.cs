using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{
    public GameObject worldMapCanvas;
    public GameObject instruction;

    bool near = false;
    public bool activated = false;
    string interactionColliderTag = "InteractionCollider";
    
    public string identity;

    StateManager stateManager;
    // Start is called before the first frame update
    void Start()
    {
        stateManager = Toolbox.GetInstance().GetStateManager();
        if (IsEnabled())
        {
            GetComponent<Animator>().SetTrigger("visited");
        }

        string startSpot = stateManager.GetTmpState().loadSpot;
        if (startSpot.Length > 0)
        {
            GameObject pod = GameObject.Find(startSpot + "Pod");
            Vector3 podPos = pod.transform.position;
            GameObject.Find("body").transform.position = podPos;
        }

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

    void Refresh()
    {
        GameState gs = stateManager.GetGameState();

        Button[] buttons = gameObject.GetComponentsInChildren<Button>();
        foreach (Button b in buttons)
        {
            bool enabled = (bool) gs.GetType().GetField(b.name).GetValue(gs);
            if (enabled)
            {
                b.interactable = true;
            }
            else
            {
                b.interactable = false;
            }

            if (b.name == identity)
            {
                Color newColor = new Color(0.5137255f, 1f, 0.9058824f, 1f);

                b.GetComponent<Image>().color = newColor;
            }
        }
    }

    void Enable()
    {
        gameObject.GetComponent<Animator>().SetTrigger("enable");
        GameState gs = stateManager.GetGameState();
        gs.GetType().GetField(identity).SetValue(gs, true);
    }

    bool IsEnabled()
    {
        //print(stateManager);
        GameState gs = stateManager.GetGameState();

        bool enabled = (bool)gs.GetType().GetField(identity).GetValue(gs);
        return enabled;
    }

    void Activate()
    {
        

        Refresh();
        CanvasGroup cg = worldMapCanvas.GetComponent<CanvasGroup>();
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
        activated = true;
        stateManager.GetTmpState().preventGameInput = true;
    }

    void Deactivate()
    {
        CanvasGroup cg = worldMapCanvas.GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.blocksRaycasts = false;
        cg.interactable = false;
        activated = false;
        stateManager.GetTmpState().preventGameInput = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {  
        if (other.CompareTag(interactionColliderTag))
        {
            instruction.GetComponent<CanvasGroup>().alpha = 1;
            near = true;
            if (!IsEnabled())
            {
                Enable();
            }
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(interactionColliderTag)) {
            near = false;
            instruction.GetComponent<CanvasGroup>().alpha = 0;
            Deactivate();
        }
    }

    public void Teleport(string destination)
    {
        Deactivate();
        stateManager.Teleport(destination);
    }

}
