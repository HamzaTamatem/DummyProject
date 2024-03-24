using System.Xml.Serialization;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    //AudioSource myAudio => GetComponent<AudioSource>();

    [SerializeField] [TextArea] string[] arDialogues;
    [SerializeField] [TextArea] string[] enDialogues;
    [SerializeField] string enCharacterName;
    [SerializeField] string arCharacterName;
    [SerializeField] Sprite portraitImg;

    //Extra
    public string[] enNames;
    public string[] arNames;
    public Sprite[] portraits;

    DialogueUI dialogueUI;
    GameObject sign;

    bool canTalk;

    [SerializeField] GameObject activeAfterDialogue;

    private void Awake()
    {
        sign = transform.Find("Sign").gameObject;
        dialogueUI = FindObjectOfType<DialogueUI>();
    }

    private void Update()
    {
        //Change input to match mobile input
        if (canTalk)
        {
            switch (dialogueUI.language)
            {
                case DialogueUI.Languages.en:

                    dialogueUI.StartDialogue(enCharacterName, portraitImg, enDialogues, this);

                    break;
                case DialogueUI.Languages.ar:

                    dialogueUI.StartDialogue(arCharacterName, portraitImg, arDialogues, this);

                    break;
            }
            StopTalking();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //sign.SetActive(true);
            GetComponent<Collider2D>().enabled = false;
            canTalk = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StopTalking();
        }
    }

    private void StopTalking()
    {
        PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerMovement.StopPlayer(true);

        sign.SetActive(false);
        canTalk = false;
    }

    public void ActiveAfterDialogue()
    {
        if (!activeAfterDialogue)
            return;

        activeAfterDialogue.SetActive(true);
    }
}
