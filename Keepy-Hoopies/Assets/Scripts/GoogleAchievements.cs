using UnityEngine;
using GooglePlayGames;

public class GoogleAchievements : MonoBehaviour
{
    public void OpenAchievementPanel()
    {
        Social.ShowAchievementsUI();
    }

    public void CheckScoreAchievements(float score)
    {
        if (Social.localUser.authenticated)
        {
            if (score >= 10.0f)
            {
                Social.ReportProgress(GPGSIds.achievement_beginner, 100f, (bool success) =>
                {
                    Debug.Log("Swish achievement unlocked");
                });
            }
            if (score >= 20.0f)
            {
                Social.ReportProgress(GPGSIds.achievement_getting_better, 100f, (bool success) =>
                {
                    Debug.Log("Swish achievement unlocked");
                });
            }
            if (score >= 50.0f)
            {
                Social.ReportProgress(GPGSIds.achievement_hey_thats_pretty_good, 100f, (bool success) =>
                {
                    Debug.Log("Swish achievement unlocked");
                });
            }
            if (score >= 100.0f)
            {
                Social.ReportProgress(GPGSIds.achievement_now_thats_a_good_score, 100f, (bool success) =>
                {
                    Debug.Log("Swish achievement unlocked");
                });
            }
        }
    }

    public void UnlockSwish()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(GPGSIds.achievement_swish, 100f, (bool success) =>
            {
                Debug.Log("Swish achievement unlocked");
            });
        }
    }

    public void UnlockHoop()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(GPGSIds.achievement_hoop, 100f, (bool success) =>
            {
                Debug.Log("Swish achievement unlocked");
            });
        }
    }

    public void UnlockDoubleHoop()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(GPGSIds.achievement_double_hooper, 100f, (bool success) =>
            {
                Debug.Log("Swish achievement unlocked");
            });
        }
    }

    public void UnlockDoubleSwish()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(GPGSIds.achievement_double_swisher, 100f, (bool success) =>
            {
                Debug.Log("Swish achievement unlocked");
            });
        }
    }
}
