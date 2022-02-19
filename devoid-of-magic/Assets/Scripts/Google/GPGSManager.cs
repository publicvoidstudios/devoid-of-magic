using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using TMPro;

using GooglePlayGames.BasicApi.SavedGame;
using System;
public class GPGSManager : MonoBehaviour
{
    private PlayGamesClientConfiguration clientConfiguration;
    [Header("Assign only in main menu")]
    public TMP_Text statusText;
    public TMP_Text playerName;
    public TMP_Text playerID;
    [SerializeField]
    GameObject[] signButtons;
    [SerializeField]
    bool inMainMenu;
    [Header("Google buttons, only main menu")]
    [SerializeField]
    GameObject achievementsButton;
    [SerializeField]
    GameObject leaderboardButton;
    [Header("Reference to Player (ALWAYS ASSIGN)")]
    [SerializeField]
    Player player;

    void Start()
    {
        if (!Social.localUser.authenticated)
        {
            ConfigureGPGS();
            SignInToGPGS(SignInInteractivity.CanPromptOnce, clientConfiguration);
            Invoke("LoadDelayed", 5f);
        }
        else
        {
            Invoke("LoadDelayed", 5f);
        }
    }

    private void LoadDelayed()
    {
        OpenSave(false);
    }

    internal void ConfigureGPGS()
    {
        clientConfiguration = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
            .Build();
    }

    internal void SignInToGPGS(SignInInteractivity interactivity, PlayGamesClientConfiguration configuration)
    {
        configuration = clientConfiguration;
        PlayGamesPlatform.InitializeInstance(configuration);
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(interactivity, (code) =>
        {
            if (inMainMenu)
            {
                statusText.text = "Authenticating...";
                if (code == SignInStatus.Success)
                {
                    statusText.text = "Connected to Play Games";
                    playerName.text = "USER: " + Social.localUser.userName;
                    playerID.text = "ID: " + Social.localUser.id;
                    //Activate all stuff working only with Google
                    GoogleButtons(true);
                    //Activate SignOut button
                    signButtons[0].SetActive(false);
                    signButtons[1].SetActive(true);
                }
                else
                {
                    statusText.text = "Failed to authenticate";
                    if(player.player_name != "")
                    {
                        playerName.text = "USER: " + player.player_name;
                    }
                    else
                    {
                        playerName.text = "USER: " + "New Adventurer";
                    }                    
                    playerID.text = "Failure reason: " + code;
                    //Deactivate Google's buttons
                    GoogleButtons(false);
                    //Activate SignIn button
                    signButtons[0].SetActive(true);
                    signButtons[1].SetActive(false);
                }
            }            
        });
    }

    public void SignInButton()
    {
        SignInToGPGS(SignInInteractivity.CanPromptAlways, clientConfiguration);
    }

    public void SignOutButton()
    {
        PlayGamesPlatform.Instance.SignOut();
        statusText.text = "Signed out";
        playerName.text = "USER: " + "New Adventurer";
        playerID.text = "ID: " + "null";
        //Deactivate Google buttons
        GoogleButtons(false);
    }

    public void ShowAchievementsUI()
    {
        Social.ShowAchievementsUI();
    }

    public void ShowLeaderboardUI()
    {
        Social.ShowLeaderboardUI();
    }

    public void GrantBasicAchievement(string achievementID)
    {
        Social.ReportProgress(achievementID,
            100.0f,
            (bool success) =>
            {
                if (success)
                {
                    //Do something when got achievement
                }
            });
    }

    public void ReportLeaderboardScore(int score, string leaderboardID)
    {
        Social.ReportScore(score, leaderboardID, (bool success) => { });
    }

    private void GoogleButtons(bool state)
    {
        if(achievementsButton != null)
            achievementsButton.SetActive(state);
        if(leaderboardButton != null)
            leaderboardButton.SetActive(state);
    }

    #region SavedGames

    private bool isSaving;

    public void OpenSave(bool saving)
    {     
        if (Social.localUser.authenticated)
        {
            isSaving = saving;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution("DevoidSave", DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, SaveGameOpen);
        }
        //else
        //{
        //    if (saving)
        //    {
        //        player.Save();
        //    }
        //}
    }

    private void SaveGameOpen(SavedGameRequestStatus status, ISavedGameMetadata metadata)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            if (isSaving) //Saving
            {
                //Convert Data types to byte array
                byte[] byteArrayData = System.Text.Encoding.ASCII.GetBytes(GetSaveString());
                //Update metadata
                SavedGameMetadataUpdate updateForMetadata = new SavedGameMetadataUpdate.Builder().WithUpdatedDescription("Savefile updated at: " + DateTime.Now.ToString()).Build();
                //Commmit Save
                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(metadata, updateForMetadata, byteArrayData, SaveCallback);
            }
            else //Loading
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(metadata, LoadCallBack);
            }
        }
        
    }

    private void LoadCallBack(SavedGameRequestStatus status, byte[] data)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            string loadedData = System.Text.Encoding.ASCII.GetString(data);
            //Check if loaded data has more totalkills than local
            if(CompareData(loadedData))
                LoadSavedString(loadedData); //Convert loadedData to readable
        }
    }

    public void LoadSavedString(string cloudData)
    {
        string[] cloudStringArray = cloudData.Split('|');
        player.level = int.Parse(cloudStringArray[0]);
        player.sublevel = int.Parse(cloudStringArray[1]);
        player.progress = int.Parse(cloudStringArray[2]); 
        player.axeLevel = int.Parse(cloudStringArray[3]);
        player.bowLevel = int.Parse(cloudStringArray[4]);
        player.armorLevel = int.Parse(cloudStringArray[5]);
        player.strengthLevel = int.Parse(cloudStringArray[6]);
        player.gold = int.Parse(cloudStringArray[7]);
        player.player_name = cloudStringArray[8];
        player.tools = int.Parse(cloudStringArray[9]);
        player.currentArmor = int.Parse(cloudStringArray[10]);
        player.magicDamage = int.Parse(cloudStringArray[11]);

        ////Save progress locally
        player.Save();
    }

    public string GetSaveString()
    {
        string dataToSave = "";
           
        dataToSave += player.level.ToString();
        dataToSave += "|";
        dataToSave += player.sublevel.ToString();
        dataToSave += "|";
        dataToSave += player.progress.ToString();
        dataToSave += "|";
        dataToSave += player.axeLevel.ToString();
        dataToSave += "|";
        dataToSave += player.bowLevel.ToString();
        dataToSave += "|";
        dataToSave += player.armorLevel.ToString();
        dataToSave += "|";
        dataToSave += player.strengthLevel.ToString();
        dataToSave += "|";
        dataToSave += player.gold.ToString();
        dataToSave += "|";
        dataToSave += player.player_name;
        dataToSave += "|";
        dataToSave += player.tools.ToString();
        dataToSave += "|";
        dataToSave += player.currentArmor.ToString();
        dataToSave += "|";
        dataToSave += player.magicDamage.ToString();

        //Save progress locally
        player.Save();

        return dataToSave;
    }

    private void SaveCallback(SavedGameRequestStatus status, ISavedGameMetadata metadata)
    {
        if(status == SavedGameRequestStatus.Success)
        {

        }
    }

    #endregion

    #region No/Old LocalDataHandler

    private bool CompareData(string loadedData)
    {
        bool cloudDataBetter;

        //Get player's totalkills from loaded data
        string[] cloudStringArray = loadedData.Split('|');
        int cloudCompareData = int.Parse(cloudStringArray[2]); //Progress from cloud
        int localCompareData = player.progress;

        if (cloudCompareData > localCompareData)
        {
            cloudDataBetter = true;
        }
        else
        {
            cloudDataBetter = false;
        }

        return cloudDataBetter;
    }

    #endregion

}
