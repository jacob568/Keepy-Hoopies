using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveJoystickToTouchLocation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private LeftJoystick leftJoystick;
    private Button panelButton;
    private bool isHeld;
    // Start is called before the first frame update
    void Start()
    {
        isHeld = false;
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
    }

    public bool GetIsHeld()
    {
        return isHeld;
    }
}
