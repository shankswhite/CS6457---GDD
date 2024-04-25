using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMemuUI;
    // Start is called before the first frame update
    void Start()
    {
        pauseMemuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (GameIsPaused) {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Resuming");
        pauseMemuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        Debug.Log("Pausing");
        pauseMemuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }


    public void Return()
    {
        SceneManager.LoadScene("Title Screen - Climb and Conquer");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
