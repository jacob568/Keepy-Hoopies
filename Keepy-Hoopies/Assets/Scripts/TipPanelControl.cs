using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TipPanelControl : MonoBehaviour
{
    public TextMeshProUGUI[] PanelTexts;
    private bool faded;
    // Start is called before the first frame update
    void Start()
    {
        faded = false;
        PanelTexts = GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void FadeText()
    {
        faded = true;
        foreach (TextMeshProUGUI text in PanelTexts)
        {
            if (text.IsActive())
            {
                text.CrossFadeAlpha(0.0f, 2.5f, false);
            }
        }
    }

    public void ShowText()
    {
        foreach (TextMeshProUGUI text in PanelTexts)
        {
            text.CrossFadeAlpha(255f, 0f, true);
        }
    }

    public void HideText()
    {
        foreach (TextMeshProUGUI text in PanelTexts)
        {
            if (text.IsActive())
            {
                text.CrossFadeAlpha(0.0f, 0f, true);
            }
        }
        faded = true;
    }
    
    public bool GetFaded()
    {
        return faded;
    }
}
