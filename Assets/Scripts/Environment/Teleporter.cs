using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{
    public GameObject worldMapCanvas;
    bool near = false;
    bool activated = false;
    string interactionColliderTag = "InteractionCollider";

    public string identity;

    // Start is called before the first frame update
    void Start()
    {
        string startSpot = StateManager.GetTmpState().loadSpot;
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
        GameState gs = StateManager.GetGameState();

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

    void Activate()
    {
        Refresh();
        CanvasGroup cg = worldMapCanvas.GetComponent<CanvasGroup>();
        cg.alpha = 1;
        cg.blocksRaycasts = true;
        cg.interactable = true;
        activated = true;
        StateManager.GetTmpState().preventGameInput = true;
    }

    void Deactivate()
    {
        CanvasGroup cg = worldMapCanvas.GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.blocksRaycasts = false;
        cg.interactable = false;
        activated = false;
        StateManager.GetTmpState().preventGameInput = false;
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

    public void Teleport(string destination)
    {
        Deactivate();
        StateManager.Teleport(destination);
    }

}
