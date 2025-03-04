using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="Dialogue_", menuName ="ScriptableObjects/Dialogues/Character Dialogue", order = 0)]
public class Dialogue: ScriptableObject
{
    public int StartTimeCode;
    public int PersoID;
    public int EndTimeCode;
    public AudioClip Voice;

    public string TextFr;
    public string TextEn;
}
