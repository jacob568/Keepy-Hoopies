﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour {

    public TextMeshProUGUI countdownText;
    public TipPanelControl[] tipPanels;
    private GameManagerScript gameManager;
    private bool countdownRunning;
    private IEnumerator co;
    private bool rewardedAdRound;

	// Use this for initialization
	void Start () {
        gameManager = GetComponent<GameManagerScript>();
        co = countdown();
        countdownRunning = false;
        countdownText.text = "Ready?";
        rewardedAdRound = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)
                && !countdownRunning
                && gameManager.getGameReadyState()
                && !gameManager.getPausedState()
                && !gameManager.getGameState()
            )
        {
            foreach (TipPanelControl tipPanel in tipPanels)
            {
                if (tipPanel.isActiveAndEnabled)
                {
                    tipPanel.FadeText();
                }
            }
            countdownRunning = true;
            co = countdown();
            StartCoroutine(co);
        }
	}

    IEnumerator countdown()
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(1f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);
        countdownText.text = "GO!";
        countdownRunning = false;
        if (rewardedAdRound)
        {
            gameManager.StartRewardedAdRound();
            rewardedAdRound = false;
        }
        else
        { 
            gameManager.startGame();
        }
        yield return new WaitForSeconds(1f);
        countdownText.text = "";
    }
    
    public void refresh()
    {
        if (countdownRunning) {
            StopCoroutine(co);
        }
        countdownRunning = false;
        countdownText.text = "Ready?";
    }

    public void IsRewardedAdRound()
    {
        rewardedAdRound = true;
    }
}
