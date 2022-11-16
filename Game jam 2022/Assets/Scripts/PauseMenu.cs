using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown("escape")) {
            if(isPaused == true) {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                isPaused = false;
            }
            else if(isPaused == false) {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                isPaused = true;
            }
        }
    }

    public void BackToMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
