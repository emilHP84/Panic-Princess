using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EVENTS.InvokeOnCameraFollow(true);
    }

    private void OnTriggerExit(Collider other)
    {
        EVENTS.InvokeOnCameraFollow(false);
    }
}
