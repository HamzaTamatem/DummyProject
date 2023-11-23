using System;
using System.Collections;
using System.Text;
using RTLTMPro;
using UnityEngine;

public class ArabicTypeWriterEffect : MonoBehaviour
{

    private RTLTextMeshPro RTLText;
    [Tooltip("The text that will be typed.")]
    [SerializeField] private string textToTypeWrite;
    [Tooltip("The time it takes to type every character.")]
    [SerializeField] private float timeBetweenChars;

    private void Awake()
    {
        RTLText = GetComponent<RTLTextMeshPro>();
    }

    void Start()
    {
        /*// Convert Unicode to text
        string text = UnicodeToString(unicodeString);

        // Output the result
        Debug.Log("Converted Text: " + text);
        RTLText.text = text;*/

        // TRYING TO CONVERT UNICODE TO CHARACTERS
        // string letter = UnicodeToChar(unicodeString, 2);
        // Debug.Log($"Converter string: {letter}");
        // RTLText.text = letter;

        StartCoroutine(TypeWrite(textToTypeWrite));
    }

    private IEnumerator TypeWrite(string arabicText)
    {
        // Unicode string in the format 0xXXXX
        // string unicodeString = "0x0645 0x0631 0x062d 0x0628 0x0627"; // This represents the word "مرحبا" in Unicode
        if (arabicText.Length <= 0)
        {
            Debug.LogWarning("Text to type write is empty.");
            yield break;
        }
        
        string unicodeString = ArabicStringToUnicodeString(arabicText);
        
        for (int i = 0; i < arabicText.Length; i++)
        {
            string letter = UnicodeToChar(unicodeString, i+1);
            Debug.Log($"Converter string: {letter}");
            RTLText.text = letter;
            yield return new WaitForSeconds(timeBetweenChars);
        }
    }

    string UnicodeToString(string unicodeString)
    {
        // Split the Unicode string into separate Unicode values
        string[] unicodeArray = unicodeString.Split(' ');

        // Convert each Unicode value to its corresponding character
        StringBuilder sb = new StringBuilder();
        foreach (string unicode in unicodeArray)
        {
            if (unicode.StartsWith("0x"))
            {
                // Remove "0x" and parse the hexadecimal value
                int codePoint = Convert.ToInt32(unicode.Substring(2), 16);
                sb.Append(char.ConvertFromUtf32(codePoint));
            }
            else
            {
                // Handle non-Unicode values (e.g., spaces or other characters)
                sb.Append(unicode);
            }
        }

        return sb.ToString();
    }
    
    string UnicodeToChar(string unicodeString, int amountOfLetters)
    {
        StringBuilder sb = new StringBuilder();
        
        // Split the Unicode string into separate Unicode values
        string[] unicodeArray = unicodeString.Split(' ');

        for (int i = 0; i < amountOfLetters; i++)
        {
            string unicode = unicodeArray[i];
            if (unicode.StartsWith("0x"))
            {
                // Remove "0x" and parse the hexadecimal value
                int codePoint = Convert.ToInt32(unicode.Substring(2), 16);
                sb.Append(char.ConvertFromUtf32(codePoint));
            }
            else
            {
                // Handle non-Unicode values (e.g., spaces or other characters)
                sb.Append(unicode);
            }
        }
        
        return sb.ToString();
    }

    private string ArabicStringToUnicodeString(string arabicWord)
    {
        // Create a StringBuilder to build the Unicode string
        StringBuilder sb = new StringBuilder();

        // Iterate through each character in the Arabic word
        foreach (char c in arabicWord)
        {
            // Append the Unicode representation of the character with a space
            sb.AppendFormat("0x{0:X} ", (int)c);
        }

        Debug.Log($"{nameof(ArabicStringToUnicodeString)}: {sb.ToString().Trim()}");

        // Remove the trailing space and return the result
        return sb.ToString().Trim();
    }
}