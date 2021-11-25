using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject loadingPanel;
    public Slider slider;
    public TMP_Text percentage;
    public TMP_Text interactive;
    public Button continueButton;
    bool activationAllowed;
   public void LoadLevel(int buildIndex)
    {
        Time.timeScale = 1f;
        AudioManager audio = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
        audio.Play("Click");
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }        
        StartCoroutine(LoadAsyncronously(buildIndex));
    }

    IEnumerator LoadAsyncronously(int buildIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Single);
        operation.allowSceneActivation = false;
        loadingPanel.SetActive(true);
        continueButton.interactable = false;
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            percentage.text = (progress * 100f).ToString("F0") + "/100";
            if(0 <= progress && progress > 0.1f)
            {
                interactive.text = "Initializing...";
            }
            if (0.1f <= progress && progress > 0.25f)
            {
                interactive.text = "Sharpening axe...";
            }
            if (0.25f <= progress && progress > 0.6f)
            {
                interactive.text = "Stealing arrows...";
            }
            if (0.6f <= progress && progress >= 1f)
            {
                interactive.text = "Press Continue button";
            }
            if (operation.progress >= 0.9f)
            {
                continueButton.interactable = true;
                if (activationAllowed)
                    //Activate the Scene
                    operation.allowSceneActivation = true;
            }
            yield return null;
        }

        
    }

    public void Continue()
    {
        AudioManager audio = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
        audio.Play("Click");
        activationAllowed = true;
    }
}
