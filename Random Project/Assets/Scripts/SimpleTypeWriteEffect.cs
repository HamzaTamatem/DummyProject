using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTypeWriteEffect : MonoBehaviour
{

    [SerializeField] private string textToTypeWrite = "السلام عليكم";
    [SerializeField] private float timeBetweenChars;
    
    [SerializeField] private RTLTMPro.RTLTextMeshPro textToFill;

    
    // Start is called before the first frame update
    void Start()
    {
        TypeWrite(timeBetweenChars);
    }

    public void TypeWrite(float timeBetweenChars)
    {
        StartCoroutine(TypeWriteCoroutine(timeBetweenChars));
    }

    private IEnumerator TypeWriteCoroutine(float timeBetweenChars)
    {
        for (int i = 0; i <= textToTypeWrite.Length; i++)
        {
            textToFill.text = textToTypeWrite.Substring(0, i);
            yield return new WaitForSeconds(timeBetweenChars);
        }
    }
}
