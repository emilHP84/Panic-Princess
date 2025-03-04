using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="Dialogue_", menuName ="ScriptableObjects/Dialogues/Character Dialogue", order = 0)]
public class Dialogue: ScriptableObject
{
    public float StartTimeCode;
    public int PersoID;
    public float Duration;
    public AudioClip Voice;

    public string TextFr;
    public string TextEn;
}
