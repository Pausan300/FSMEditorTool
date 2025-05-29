using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject m_ResumeButton;
    public GameObject m_RestartButton;
    public GameObject m_ReturnToMainMenuButton;

    public GameObject m_PauseText;
    public GameObject m_GameOverText;
    public GameObject m_WinText;


    void Start()
    {
        SetNormalPause();
        gameObject.SetActive(false);
    }

    public void SetNormalPause() 
    {
        m_PauseText.SetActive(true);
        m_GameOverText.SetActive(false);
        m_WinText.SetActive(false);

        m_ResumeButton.SetActive(true);
        m_RestartButton.SetActive(false);
        m_ReturnToMainMenuButton.SetActive(true);
    }
    public void SetGameOver() 
    {
        m_PauseText.SetActive(false);
        m_GameOverText.SetActive(true);
        m_WinText.SetActive(false);

        m_ResumeButton.SetActive(false);
        m_RestartButton.SetActive(true);
        m_ReturnToMainMenuButton.SetActive(true);
    }
    public void SetWin() 
    {
        m_PauseText.SetActive(false);
        m_GameOverText.SetActive(false);
        m_WinText.SetActive(true);

        m_ResumeButton.SetActive(false);
        m_RestartButton.SetActive(true);
        m_ReturnToMainMenuButton.SetActive(true);
    }

    public void OpenMenu() 
    {
        Time.timeScale=0.0f;
        gameObject.SetActive(true);
        Cursor.lockState=CursorLockMode.None;
    }
    public void CloseMenu() 
    {
        Time.timeScale=1.0f;
        gameObject.SetActive(false);
        Cursor.lockState=CursorLockMode.Locked;
    }

    public void LoadScene(string SceneName) 
    {
        Time.timeScale=1.0f;
        SceneController.m_Instance.LoadScene(SceneName);
    }

    public void ReturnToMainMenu() 
    {
        Time.timeScale=1.0f;
        SceneController.m_Instance.LoadScene("MainMenuScene");
    }
}
