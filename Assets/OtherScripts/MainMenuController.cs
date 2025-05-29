using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void LoadScene(string SceneName) 
    {
        SceneController.m_Instance.LoadScene(SceneName);
    }
}
