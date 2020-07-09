using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Button playButton;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(StartGame);
        scoreText.text = PlayerPersistence.GetHighScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
