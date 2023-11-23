using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ArabicTypeWrite : MonoBehaviour
{

    private TMP_Text arabicText;
    [SerializeField] private float timeBetweenCharacters;

    private void Awake()
    {
        arabicText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        TypeWrite(timeBetweenCharacters);
    }

    public void TypeWrite(float timeBetweenChars)
    {
        StartCoroutine(TypeWriteCoroutine(timeBetweenChars));
    }
    
    private IEnumerator TypeWriteCoroutine(float timeBetweenChars)
    {
        string textToType = arabicText.text;
        Debug.Log($"arabicText.text: {arabicText.text} | textToType: {textToType}");
        arabicText.text = string.Empty;
        for (int i = textToType.Length - 1; i >= 0; i--)
        {
            Debug.Log(textToType[i]);
            Debug.Log(GetCharUnicode(textToType[i]));
            arabicText.text += textToType[i];
            arabicText.text += " ";
            arabicText.text = ArabicSupport.Fix(arabicText.text);
            yield return new WaitForSeconds(timeBetweenChars);
        }
    }

    private int GetCharUnicode(char myChar)
    {
        // Get the Unicode code point of the character
        int unicode = char.ConvertToUtf32(myChar.ToString(), 0);

        // Print the result in the format 0x
        Debug.Log($"Unicode code point of '{myChar}': 0x{unicode:X}");

        return unicode;
    }
}
