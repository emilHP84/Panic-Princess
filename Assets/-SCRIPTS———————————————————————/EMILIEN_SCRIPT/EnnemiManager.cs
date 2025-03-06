using UnityEngine;
using UnityEngine.SceneManagement;

public class EnnemiManager : MonoBehaviour
{
    public GameObject FXPrefab;
    public GameObject FXHited;

    public LayerMask colidWith;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            EVENTS.InvokeHited();
            Instantiate(FXHited, transform.position, Quaternion.identity, transform);
            SceneLoader.access.LoadScene(SceneManager.GetActiveScene().buildIndex, 1, 1, 1, false, 0.5f);
        }

        if (other.gameObject.layer == 20)
        {
            EnnemiVoice();
        }
    }

    void EnnemiVoice()
    {
        Instantiate(FXPrefab, transform.position, Quaternion.identity, transform);
    }
}
