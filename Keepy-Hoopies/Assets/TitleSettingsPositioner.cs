using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSettingsPositioner : MonoBehaviour
{
    private bool isOpen;
    public Transform openPosition, closedPosition;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isInPosition());
        if (!isInPosition())
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, isOpen ? openPosition.localPosition : closedPosition.localPosition, 500f * Time.deltaTime);
        }
    }

    public bool GetIsOpen()
    {
        return isOpen;
    }

    private bool isInPosition()
    {
        if (isOpen && (transform.position.y > openPosition.position.y - 0.1f && transform.position.y < openPosition.position.y + 0.1f))
        {
            return true;
        } else if (!isOpen && (transform.position.y > closedPosition.position.y - 0.1f && transform.position.y < closedPosition.position.y + 0.1f))
        {
            return true;
        }
        return false;
    }

    public void OpenPanel()
    {
        isOpen = true;
    }
    public void ClosePanel()
    {
        isOpen = false;
    }
}
