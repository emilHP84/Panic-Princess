using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateSwitcher : MonoBehaviour
{
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            GAME.MANAGER.SwitchTo(State.menu);
            MENU.SCRIPT.BackToMainMenu();
        }
    }
}
