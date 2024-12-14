using Maxime.Script.Enum;
using UnityEngine;
using UnityEngine.Serialization;

namespace Maxime.Script.Dialog
{
    [CreateAssetMenu(fileName = "DialogSentences", menuName = "Dialog/DialogSentences", order = 0)]
    public class DialogSentences : ScriptableObject
    {
        [SerializeField] private string _frenchText;
        [SerializeField] private string _englishText;
        [SerializeField] private string _spanishText;

        public string GetLocalizedString(Language language)
        {
            switch (language)
            {
                case Language.Francais:
                    return _frenchText; 
                case Language.English:
                    return _englishText; 
                case Language.Espagnol:
                    return _spanishText;
                default:
                    return "Default Text"; 
            }
        }
    }
}