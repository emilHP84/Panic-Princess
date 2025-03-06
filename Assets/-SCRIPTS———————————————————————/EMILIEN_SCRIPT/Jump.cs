using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private AnimationCurve jumpCurve = new AnimationCurve();
    [SerializeField] private float jumpHeight = 2f;

    public void StartJumping(AudioSource source, GameObject jump, GameObject run)
    {
        source.Stop();
        jump.SetActive(true);
        run.SetActive(false);
    }

    public void DuringJumping(float startJumpingTime, float jumpDuration)
    {
        float elapsedTime = Time.time - startJumpingTime;
        float normalizedTime = elapsedTime / jumpDuration;
        float curveValue = jumpCurve.Evaluate(normalizedTime);

        Vector3 newPosition = transform.position;
        newPosition.y =  curveValue * jumpHeight;

        transform.position = newPosition;
    }

    public void EndJumping(AudioSource source, GameObject obj, GameObject run)
    {
        source.Play();
        obj.SetActive(false);
        run.SetActive(true);
    }
}
