using UnityEngine;
using UnityEngine.SceneManagement;

public class EnnemiManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EVENTS.InvokeHited();
        SceneLoader.access.LoadScene(SceneManager.GetActiveScene().buildIndex,1,1,1,false,0.5f);
    }
}
