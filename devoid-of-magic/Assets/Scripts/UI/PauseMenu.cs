using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    [SerializeField]
    public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
    public void LoadVillage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    public void LoadMap()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Map", LoadSceneMode.Single);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
