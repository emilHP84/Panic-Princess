
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosManager : MonoBehaviour
{
    [SerializeField] private GameObject camPos;
    float offsetX;
    float Ypos;

    private bool m_enabled;
    private void OnEnable()
    {
        EVENTS.OnCameraFollow += ActualizeCameraPos;
    }

    private void Start()
    {
        //camPos.transform.position = transform.position;
        Ypos = transform.position.y;
    }

    public void ActualizeCameraPos(bool wantToFollow)
    {
        m_enabled = wantToFollow;
        if (wantToFollow == true) offsetX = camPos.transform.position.x - transform.position.x;
    }

    private void Update()
    {
        if (m_enabled == true)
        {
            camPos.transform.position = new Vector3(transform.position.x + offsetX, Ypos, transform.position.z);
        }
        else
        {
            return;
        }
    }

    private void OnDisable()
    {
        EVENTS.OnCameraFollow -= ActualizeCameraPos;

    }
}
