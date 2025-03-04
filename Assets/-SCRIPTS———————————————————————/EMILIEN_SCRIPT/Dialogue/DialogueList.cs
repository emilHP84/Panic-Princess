using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogues_level_", menuName = "ScriptableObjects/Dialogues/Dialogues Level", order = 0)]

public class DialogueList : ScriptableObject
{
    public List<Dialogue> Dialogues;
}