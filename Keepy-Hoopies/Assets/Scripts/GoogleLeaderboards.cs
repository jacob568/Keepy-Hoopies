using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using TMPro;

public class GoogleLeaderboards : MonoBehaviour
{
    public void updateHighScoreLeaderboard(long score)
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ReportScore(score, GPGSIds.leaderboard_high_score, (bool success) => {
                if (success)
                {
                    Debug.Log("Added high score to leaderboard");
                }
                else
                {
                    Debug.Log("Failed to add high score to leaderboard");
                }
            });
        }
    }

    public void updateHoopHighScoreLeaderboard(long score)
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ReportScore(score, GPGSIds.leaderboard_most_hoops_in_a_game, (bool success) => {
                if (success)
                {
                    Debug.Log("Added high score to leaderboard");

                }
                else
                {
                    Debug.Log("Failed to add high score to leaderboard");
                }
            });
        }
    }

    public void updateSwishHighScoreLeaderboard(long score)
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ReportScore(score, GPGSIds.leaderboard_most_swishes_in_a_game, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Added high score to leaderboard");

                }
                else
                {
                    Debug.Log("Failed to add high score to leaderboard");
                }
            });
        }
    }
}
