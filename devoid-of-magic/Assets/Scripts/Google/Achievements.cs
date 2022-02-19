using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Achievements : MonoBehaviour
{
    [SerializeField]
    GPGSManager gPGSManager;

    [SerializeField]
    Player player;

    private IEnumerator Start()
    {
        while (!Social.localUser.authenticated)
        {
            yield return new WaitForSeconds(0.5f);
        }
        AchievementsReport();
    }


    public void AchievementsReport()
    {
        CheckAchievements();
        ReportLeaderboards();
    }

    private void ReportLeaderboards()
    {
        gPGSManager.ReportLeaderboardScore(player.gold, GPGSIds.leaderboard_postponed_for_retirement);
    }

    private void CheckAchievements()
    {
        if (player.level >= 1)
        {
            gPGSManager.GrantBasicAchievement(GPGSIds.achievement_adventure_begins);
        }
        if (player.strengthLevel >= 10)
        {
            gPGSManager.GrantBasicAchievement(GPGSIds.achievement_brutal);
        }
        if (player.progress >= 3)
        {
            gPGSManager.GrantBasicAchievement(GPGSIds.achievement_defeat_bubarossa);
        }
        if (player.progress >= 9)
        {
            gPGSManager.GrantBasicAchievement(GPGSIds.achievement_defeat_demon_lord);
        }
        if (player.progress >= 6)
        {
            gPGSManager.GrantBasicAchievement(GPGSIds.achievement_defeat_polar_ursa);
        }
        if (player.bowLevel >= 10)
        {
            gPGSManager.GrantBasicAchievement(GPGSIds.achievement_fabulous_marksman);
        }
        if (player.axeLevel >= 10)
        {
            gPGSManager.GrantBasicAchievement(GPGSIds.achievement_fearsome_weapon);
        }
        if (player.armorLevel >= 10)
        {
            gPGSManager.GrantBasicAchievement(GPGSIds.achievement_impenetrable_arapace);
        }
        if (player.magicDamage >= 1)
        {
            gPGSManager.GrantBasicAchievement(GPGSIds.achievement_wizards_trainee);
        }
    }
}
