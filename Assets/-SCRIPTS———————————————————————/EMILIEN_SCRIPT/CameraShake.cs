using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float m_shakeDuration = 1f;
    [SerializeField] float m_shakeStrenght = 1f;
    [SerializeField] int m_shakeVibrato = 10;
    [SerializeField] float m_shakeRandomness = 90;
    [SerializeField] bool m_shakeSnapping = false;
    [SerializeField] bool m_ShadeFadeOut = true;
    [SerializeField] ShakeRandomnessMode m_ShakeRandomnessMode;

    public void Shaking()
    {
        gameObject.transform.DOShakePosition(m_shakeDuration,m_shakeStrenght,
            m_shakeVibrato, m_shakeRandomness, m_shakeSnapping, m_ShadeFadeOut,
                m_ShakeRandomnessMode);

    }
}
