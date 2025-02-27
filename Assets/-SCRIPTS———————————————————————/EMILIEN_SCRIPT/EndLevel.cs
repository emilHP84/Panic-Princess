using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public int level;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hellllllllllo toto");
        SceneLoader.access.LoadScene(level, 1, 1, 1, false, 2);
    }
}
