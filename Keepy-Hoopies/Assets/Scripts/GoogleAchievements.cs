using UnityEngine;
using GooglePlayGames;

public class GoogleAchievements : MonoBehaviour
{
    public void OpenAchievementPanel()
    {
        Social.ShowAchievementsUI();
    }

    public void UnlockSwish()
    {
        Social.ReportProgress(GPGSIds.achievement_swish, 100f, null);
    }
}
