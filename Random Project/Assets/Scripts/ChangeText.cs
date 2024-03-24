using UnityEngine;

public class ChangeText : MonoBehaviour
{
    [SerializeField] GameObject en_Text;
    [SerializeField] GameObject ar_Text;

    private void OnEnable()
    {
        DialogueUI.OnLanguageChanged += UpdateLanguage;

        UpdateLanguage();
    }
    private void OnDisable()
    {
        DialogueUI.OnLanguageChanged -= UpdateLanguage;
    }
    //private void Start()
    //{
    //    UpdateLanguage();
    //}

    private void UpdateLanguage()
    {
        if (DialogueUI.Instance.language == DialogueUI.Languages.en)
        {
            en_Text.SetActive(true);
            ar_Text.SetActive(false);
        }
        else
        {
            en_Text.SetActive(false);
            ar_Text.SetActive(true);
        }
    }
}
