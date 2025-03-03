using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    private void OnEnable()
    {
        EVENTS.OnGameplay += EnableMenu;
        EVENTS.OnGameplayExit += DisableMenu;
    }

    private void OnDisable()
    {
        EVENTS.OnGameplay -= EnableMenu;
        EVENTS.OnGameplayExit -= DisableMenu;
    }



    void EnableMenu()
    {
        if (mainMenu != null) mainMenu.SetActive(true);
    }

    void DisableMenu()
    {
        if (mainMenu != null) mainMenu.SetActive(false);
    }





}
