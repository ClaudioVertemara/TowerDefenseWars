using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;
    bool pause;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    public void TogglePause() {
        pause = !pause;

        if (pause) {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        } else {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void MainMenuButton() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGameButton() {
        Application.Quit();
    }
}
