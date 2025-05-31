using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelVictory : MonoBehaviour
{
    public PauseMenuController m_PauseMenu;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            m_PauseMenu.SetWin();
            m_PauseMenu.OpenMenu();
        }
    }
}
