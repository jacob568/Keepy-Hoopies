using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour {

    public TextMeshProUGUI countdownText;
    private GameManagerScript gameManager;
    private bool countdownRunning;
    private IEnumerator co;

	// Use this for initialization
	void Start () {
        gameManager = GetComponent<GameManagerScript>();
        co = countdown();
        countdownRunning = false;
        countdownText.text = "Ready?";
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
        gameManager.startGame();
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
}
