﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadScene(int sceneNumver)
    {
        SceneManager.LoadScene(sceneNumver);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
