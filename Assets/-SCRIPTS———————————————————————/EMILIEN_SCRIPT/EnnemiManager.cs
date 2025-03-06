using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class EnnemiManager : MonoBehaviour
{
    public GameObject FXPrefab;
    public GameObject FXHited;

    public LayerMask colidWith;
    void Start()
    {
        gameObject.transform.DOScaleY(0,0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            EVENTS.InvokeHited();
            EVENTS.InvokeDeath();
            Instantiate(FXHited, transform.position, Quaternion.identity, transform);
            SceneLoader.access.LoadScene(SceneManager.GetActiveScene().buildIndex, 1, 1, 1, false, 0.5f);
        }

        if (other.gameObject.layer == 20)
        {
            StartCoroutine("Enemmi");
            EnnemiVoice();
        }
    }

    void EnnemiVoice()
    {
        Instantiate(FXPrefab, transform.position, Quaternion.identity, transform);
    }
    private IEnumerator Enemmi()
    {
        gameObject.transform.DOScaleY(1, 0.3f);
        yield return new WaitForSeconds(0.3f);
        gameObject.transform.DOShakeScale(0.2f, 0.1f, 5, 45, true, ShakeRandomnessMode.Harmonic);
        yield return new WaitForSeconds(1f);
        gameObject.transform.DOShakeScale(0.2f, 0.1f, 5, 45, true, ShakeRandomnessMode.Harmonic);
    }
}
