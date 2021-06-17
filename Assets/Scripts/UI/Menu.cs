using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] bool GameIsPaused = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) { MenuContinue(); }
            else { MenuPause(); }
        }
    }
    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void LoadStarMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void MenuContinue()
    {
        Debug.Log("MIERDA PUTA CARRAGO");
        pauseMenuUI.SetActive(false); ;
        Time.timeScale = 1;
        GameIsPaused = false;
    }
    public void MenuPause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
