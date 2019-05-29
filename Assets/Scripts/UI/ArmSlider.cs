using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArmSlider : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerDownHandler,
    IPointerUpHandler
{
    public GameObject actionButton;
    public float radius;

    Vector2 actionButtonPos;
    Vector2 defaultPos;
    /*Vector2 lastPos;
    Vector2 startDirection;*/

    float force;
    public float forceFactor;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //lastPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        /*
        startDirection = (lastPos - actionButtonPos).normalized;
        Vector2 currentDirection = (eventData.position - actionButtonPos).normalized;
        float angle = Mathf.Clamp(Vector2.SignedAngle(startDirection, currentDirection), -45f, 45f);
        transform.RotateAround(actionButton.transform.position, Vector3.forward, angle * Time.deltaTime);
        lastPos = transform.position;
        //GUIUtility.RotateAroundPivot(angle, actionButton.transform.position);
        print(angle);
        //transform.position = new Vector2()
        */

        CalculateForce(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CalculateForce(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        radius = Vector2.Distance(transform.position, actionButton.transform.position);
        actionButtonPos = new Vector2(actionButton.transform.position.x, actionButton.transform.position.y);
        defaultPos = transform.position - actionButton.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CalculateForce(PointerEventData eventData)
    {
        Vector3 currentPos = eventData.position - actionButtonPos;
        float angle = Vector2.SignedAngle(defaultPos, currentPos);
        force = angle / 90;
        print(force);
    }
}
