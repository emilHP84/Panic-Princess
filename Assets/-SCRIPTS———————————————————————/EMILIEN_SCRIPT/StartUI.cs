using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    public float timeBeforeQuit;
    float time;

    private void Update()
    {
        time += Time.deltaTime;
        if(time >= timeBeforeQuit)
        {
            Application.Quit();
        }
    }
}
