using UnityEngine;
using UnityEngine.SceneManagement;

public class EnnemiManager : MonoBehaviour
{
    public GameObject FXPrefab;
    private void OnTriggerEnter(Collider other)
    {
        EVENTS.InvokeHited();
        SceneLoader.access.LoadScene(SceneManager.GetActiveScene().buildIndex,1,1,1,false,0.5f);
        Instantiate(FXPrefab, transform.position, Quaternion.identity,transform) ;
    }
}
