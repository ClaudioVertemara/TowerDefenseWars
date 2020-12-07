using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text gameOverText;
    public Transform towers;

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckIfWonOrLost() {
        bool noBlue = true;
        bool allBlue = true;

        for (int i = 0; i < towers.childCount; i++) {
            if (towers.GetChild(i).CompareTag("Blue")) {
                noBlue = false;
            }
            if (!towers.GetChild(i).CompareTag("Blue")) {
                allBlue = false;
            }
        }

        if (allBlue) {
            EndGame(true);
        } else if (noBlue) {
            EndGame(false);
        }
    }

    void EndGame(bool won) {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;

        if (!won) {
            gameOverText.text = "You Lost! \n:(";
        }
    }
}
