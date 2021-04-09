﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu2 : MonoBehaviour
{
    public static bool GameISPaused = false;
    public GameObject pauseMenuUI;

    public GameObject Equipment;
    public GameObject Settings;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameISPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Start()
    {
        Resume();
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameISPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("resume");
    }

    public void Pause()
    {
        Debug.Log("pause");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameISPaused = true;
        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;

        Settings.SetActive(false);
        Equipment.SetActive(false);
    }
    public void OpenSettings()
    {
        Debug.Log("Open settings");
        Settings.SetActive(true);
    }
    public void OpenEquipment()
    {
        Debug.Log("Open Equipment");
        Equipment.SetActive(true);
    }
    public void QuitToDesktop()
    {
        //Application.Quit();
        Debug.Log("Quit");
    }
    public void QuitToMenu()
    {
        //SceneManager.LoadScene("");
    }
    public void Quit()
    {

    }
}