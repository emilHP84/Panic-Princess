using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_dialogueBox;
    List<Dialogue> dialoguesList = new List<Dialogue>();
    Dialogue currentDialogue;

    private void OnEnable()
    {

    }

    private void Awake()
    {
    }
    private void Start()
    {
        EVENTS.OnSceneLoaded += SwitchDialogueScene;
    }

    private void SwitchDialogueScene(int Scene)
    {

        switch (Scene)
        {
            case 1:
                ReadXML(1);
                break;
            case 2:
                ReadXML(2);
                break;
            case 3:
                ReadXML(3);
                break;
        }

        EVENTS.OnGameplay += SelectDialogue;
    }

    private void SelectDialogue()
    {
        StartDialogue(1);
        EVENTS.OnGameplay -= SelectDialogue;
    }

    private void StartDialogue(int dialogueID)
    {
        currentDialogue = dialoguesList.FirstOrDefault(x => x.ID == dialogueID);
        if(currentDialogue != null)
        {
            StartCoroutine(NextDialogue(currentDialogue.Delay));
            StartCoroutine(ShowText());
        }
    }

    IEnumerator NextDialogue(int delay)
    {

        yield return new WaitForSeconds(delay);
        StartDialogue(currentDialogue.ID + 1);

    }

    IEnumerator ShowText()
    {
        yield return null;
        m_dialogueBox.text = currentDialogue.TextFr;
    }

    private void ReadXML(int dialogueID)
    {
        DialogueList dialogues = LoadDialoguesFromResources(dialogueID);

        if (dialogues != null)
        {
            dialoguesList = dialogues.Dialogues;
        }
        else
        {
            Debug.LogError("Impossible de charger le fichier XML.");
        }
    }

    private DialogueList LoadDialoguesFromResources(int dialogueID)
    {
        TextAsset xmlFile = Resources.Load<TextAsset>($"DialogueScene{dialogueID}");

        if (xmlFile != null)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DialogueList));
            using (System.IO.StringReader reader = new System.IO.StringReader(xmlFile.text))
            {
                return (DialogueList)serializer.Deserialize(reader);
            }
        }
        else
        {
            Debug.LogError("Fichier XML introuvable dans Resources.");
            return null;
        }
    }

    private void Update()
    {

    }
    private void OnDisable()
    {

    }
}
