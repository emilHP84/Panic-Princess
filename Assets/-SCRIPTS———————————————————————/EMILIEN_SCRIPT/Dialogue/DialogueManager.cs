using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_dialogueBox;
    List<Dialogue> dialoguesList = new List<Dialogue>();
    Dialogue currentDialogue;
    [SerializeField] AudioSource audioSource;
    int index;
    float currentTime;
    float currentSilenceTime;

    bool isDialoguePlaying;
    bool isSilenceWaiting;

    private void Start()
    {
        EVENTS.OnSceneLoaded += SwitchDialogueScene;
        audioSource.loop = false;
    }

    private void SwitchDialogueScene(int Scene)
    {

        switch (Scene)
        {
            case 1:
                SetDialogueList(1);
                break;
            case 2:
                SetDialogueList(2);
                break;
            case 3:
                SetDialogueList(3);
                break;
            case 4:
                SetDialogueList(4);
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
        Debug.Log($"début dialogue :{dialoguesList[index]} ");

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

    private void SetDialogueList(int dialogueID)
    {
        DialogueList dialogues = LoadDialoguesFromResources(dialogueID);

        if (dialogues != null)
        {
            dialoguesList = dialogues.Dialogues;
        }
    }

    private DialogueList LoadDialoguesFromResources(int dialogueID)
    {
        DialogueList dialogues = Resources.Load<DialogueList>($"Dialogues/Dialogues_level_{dialogueID}");

        if (dialogues != null && dialogues.Dialogues != null && dialogues.Dialogues.Count > 0)
        {
            return dialogues;
        }
        else
        {
            Debug.LogError("Dialogue introuvable.");
            return null;
        }
    }

    private void Update()
    {
        if (GAME.MANAGER.CurrentState == State.menu) { return; }

        currentTime += Time.deltaTime;
        if (currentDialogue != null && currentTime >= currentDialogue.StartTimeCode && !isDialoguePlaying)
        {
            Debug.Log("en train de jouer le dialogue");
            LaunchAudio();
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

    private void LaunchAudio()
    {
        audioSource.clip = currentDialogue.Voice;
        audioSource.Play();
    }

    private void OnDisable()
    {

    }
}
