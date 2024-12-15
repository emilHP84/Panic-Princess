using System;
using System.Collections.Generic;
using Maxime.Script.Enum;
using UnityEngine;
using UnityEngine.UI; // Make sure you have this for Button

namespace Maxime.Script.Dialog
{
    public class LanguageSelection : MonoBehaviour
    {
        [SerializeField] private ScriptableObject mLanguages;
        [SerializeField] private DialogSentences dialogSentences;
        [SerializeField] private Text dialogText;
        [SerializeField] private Button frenchButton;
        [SerializeField] private Button englishButton;
        [SerializeField] private Button spanishButton;


        private void Start()
        {
            /*
             frenchButton.onClick.AddListener(() => SetLanguage(Language.Francais));
                 englishButton.onClick.AddListener(() => SetLanguage(Language.English));
                 spanishButton.onClick.AddListener(() => SetLanguage(Language.Espagnol));
            */
            // default language
            SetLanguage(Language.English);
        }


        public void SetLanguage(Language language)
        {
            if (dialogSentences != null && dialogText != null)
            {
                string localizedText = dialogSentences.GetLocalizedString(language);
                dialogText.text = localizedText;
            }
            else
            {
                Debug.LogError("DialogSentences or dialogText is not assigned!");
            }
        }
    }
}