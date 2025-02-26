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
    int index;
    float currentTime;
    float currentSilenceTime;

    bool isDialoguePlaying;
    bool isSilenceWaiting;

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
        index = 0;
        currentTime = 0;
        currentSilenceTime = 0;
        isSilenceWaiting = false;
        isDialoguePlaying = false;
    }

    private void SelectDialogue()
    {
        StartDialogue();
        EVENTS.OnGameplay -= SelectDialogue;
    }

    private void StartDialogue()
    {
        if (index >= dialoguesList.Count || dialoguesList.Count <= 0) 
        { 
            currentDialogue = null;
            return;
        }

        currentDialogue = dialoguesList[index];
        
    }
    void DialogueFinish()
    {
        isDialoguePlaying = false;
        NextDialogue();
        currentSilenceTime = 0;
        isSilenceWaiting = true;
    }

    void NextDialogue()
    {
        Debug.Log("Changement dialogue");
        index++;
        StartDialogue();
    }

    void ShowText(string text)
    {
        m_dialogueBox.text = text;
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
        
        currentTime += Time.deltaTime;
        if (currentDialogue != null && currentTime >= currentDialogue.TimeCode && !isDialoguePlaying)
        {
            Debug.Log("en train de jouer le dialogue");
            ShowText(LanguageManager.currentLang == SystemLanguage.French ? currentDialogue.TextFr : currentDialogue.TextEn);
            isDialoguePlaying = true;
            Invoke("DialogueFinish", currentDialogue.Duration);
        }
        if (!isDialoguePlaying && isSilenceWaiting)
        {
            currentSilenceTime += Time.deltaTime;
            if(currentSilenceTime >= 3)
            {
                ShowText(string.Empty);
                isSilenceWaiting = false;
            }
        }
    }
    private void OnDisable()
    {

    }
}
