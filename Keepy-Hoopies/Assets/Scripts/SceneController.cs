using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void LoadLeaderboards()
    {
        SceneManager.LoadScene("LeaderboardScene");
    }
}