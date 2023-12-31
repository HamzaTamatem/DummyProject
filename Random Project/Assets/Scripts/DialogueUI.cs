using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RTLTMPro;

public class DialogueUI : MonoBehaviour
{
    public enum Languages { en,ar }
    public Languages language;

    //Refs
    GameObject dialogueBox;
    TMP_Text /*enTxt*/enName;
    [SerializeField] RTLTextMeshPro txt,arName;
    [SerializeField] Image portrait;
    //PlayerController pc;
    DialogueSystem ds;

    //Help dialogue system
    int dialogueNum;
    bool canText;
    string currentName;
    List<string> dialogues = new List<string>();

    ArabicTypeWriterEffect arabicEffect;

    [SerializeField] HorizontalLayoutGroup horizontalFolder;

    private void Awake()
    {
        dialogueBox = transform.Find("DialogueBox").gameObject;
        //enTxt = dialogueBox.transform.Find("EnTxt").GetComponent<TMP_Text>();
        enName = dialogueBox.transform.Find("EnName").GetComponent<TMP_Text>();
        //txt = dialogueBox.transform.Find("Txt").GetComponent<RTLTextMeshPro>();
        //arName = dialogueBox.transform.Find("ArName").GetComponent<RTLTextMeshPro>();
        //portrait = dialogueBox.transform.Find("Portrait").GetComponent<Image>();
        //horizontalFolder = dialogueBox.transform.Find("HorizontalFolder").GetComponent<HorizontalLayoutGroup>();

        arabicEffect = GetComponent<ArabicTypeWriterEffect>();
        //pc = FindObjectOfType<PlayerController>();

        if (language == Languages.en)
        {
            horizontalFolder.reverseArrangement = false;
            txt.alignment = TextAlignmentOptions.Left;
        }
        else
        {
            horizontalFolder.reverseArrangement = true;
            txt.alignment = TextAlignmentOptions.Right;
        }
    }

    //Call when dialogue start to assign the name, image and dialogue
    public void StartDialogue(string name,Sprite img,string[] dialogue,DialogueSystem currentDialogue)
    {
        dialogueBox.SetActive(true);
        portrait.sprite = img;

        switch (language)
        {
            case Languages.en:
                enName.text = currentName;
                break;

            case Languages.ar:
                arName.text = currentName;
                break;
        }
        
        //enTxt.text = "";
        //arTxt.text = "";
        //pc.StopMoving(true);
        //Stop player movement here
        ds = currentDialogue;

        for (int i = 0; i < dialogue.Length; i++)
        {
            dialogues.Add(dialogue[i]);
        }

        currentName = name;

        ChooseLanguage();
    }

    private void ChooseLanguage()
    {
        switch (language)
        {
            case Languages.en:
                StartCoroutine(TextDialogue(dialogues));
                break;

            case Languages.ar:
                ArabicTxt(dialogues);
                break;
        }
    }

    IEnumerator TalkingSound()
    {
        ds.gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.3f);
        ds.gameObject.GetComponent<AudioSource>().Stop();
    }

    private void ArabicTxt(List<string> dialogue)
    {
        if (dialogueNum < dialogues.Count)
        {
            UpdateInfo();
            canText = false;

            arabicEffect.rtlText = txt;
            arabicEffect.TypeWrite(dialogue[dialogueNum]);
            //arTxt.text = dialogue[dialogueNum];
            StartCoroutine(TalkingSound());

        }
        else
        {
            EndDialogue();
        }

        canText = true;
    }

    //Type writer effect
    private IEnumerator TextDialogue(List<string> dialogue)
    {
        if(dialogueNum < dialogues.Count)
        {
            ds.gameObject.GetComponent<AudioSource>().Play();
            UpdateInfo();
            canText = false;
            foreach(char letter in dialogue[dialogueNum])
            {
                txt.text += letter;
                yield return new WaitForSeconds(0.02f);
            }

            ds.gameObject.GetComponent<AudioSource>().Stop();
        }
        else
        {
            EndDialogue();
        }

        canText = true;
    }

    private void UpdateInfo()
    {
        if(ds.enNames.Length != 0)
        {
            if (ds.enNames[dialogueNum] != "")
            {
                if (language == Languages.en)
                {
                    enName.text = ds.enNames[dialogueNum];
                }
                else if(language == Languages.ar)
                {
                    arName.text = ds.arNames[dialogueNum];
                }
            }
        }
        
        if(ds.portraits.Length != 0)
        {
            if (ds.portraits[dialogueNum] != null)
            {
                portrait.sprite = ds.portraits[dialogueNum];
            }
        }
    }

    //Call when dialogue ends to close the dialogue box
    private void EndDialogue()
    {
        dialogueBox.SetActive(false);
        //pc.StopMoving(false);
        //player can move now
        dialogueNum = 0;
        //enTxt.text = "";
        txt.text = "";
        dialogues.Clear();
    }

    private void Update()
    {
        //We have to change the input key to match mobile input
        //Next dialogue
        if(Input.anyKeyDown)
        {
            if (!canText)
                return;

            dialogueNum++;
            //enTxt.text = "";
            txt.text = "";

            ChooseLanguage();
        }
    }
}
