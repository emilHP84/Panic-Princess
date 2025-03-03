using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateSwitcher : MonoBehaviour
{
    [SerializeField] float time;

    private void Start()
    {
        Invoke("MainMenu", time);
    }

    void MainMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            MENU.SCRIPT.BackToMainMenu();

        }
    }
}
