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

    private void Awake()
    {
        sign = transform.Find("Sign").gameObject;
        dialogueUI = FindObjectOfType<DialogueUI>();
    }

    private void Update()
    {
        //Change input to match mobile input
        if (canTalk && Input.GetMouseButtonDown(0))
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
            sign.SetActive(true);
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
        sign.SetActive(false);
        canTalk = false;
    }
}
