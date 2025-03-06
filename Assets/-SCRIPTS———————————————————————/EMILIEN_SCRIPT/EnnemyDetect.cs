using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyDetect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    { 
        EVENTS.InvokeHited();
    }
}
