using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    public GameObject PausedMenu;
    public string MainMenu;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("pause"))
        {   //pasue the game or unpause depending on the current state
            GamePaused();

        }
    }

    public void GamePaused()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PausedMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            PausedMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenu);
    }
}
