using UnityEngine;
using System.Collections;
using TMPro; // Assuming TextMeshPro is used for UI text
using UnityEngine.UI; // For Image component

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance { get; private set; }

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private Image characterImage; // For displaying character portraits or scene images
    [SerializeField] private float typingSpeed = 0.05f;

    private Cutscene currentCutscene;
    private int currentDialogueIndex;
    private Coroutine typingCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        dialoguePanel.SetActive(false);
    }

    public void StartCutscene(Cutscene cutscene)
    {
        currentCutscene = cutscene;
        currentDialogueIndex = 0;
        dialoguePanel.SetActive(true);
        DisplayCurrentDialogue();
    }

    public void NextDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentCutscene.dialogues[currentDialogueIndex].dialogueLine;
            typingCoroutine = null;
            return;
        }

        currentDialogueIndex++;
        if (currentDialogueIndex < currentCutscene.dialogues.Length)
        {
            DisplayCurrentDialogue();
        }
        else
        {
            EndCutscene();
        }
    }

    private void DisplayCurrentDialogue()
    {
        DialogueLine line = currentCutscene.dialogues[currentDialogueIndex];
        characterNameText.text = line.characterName;
        characterImage.sprite = line.characterImage; // Set the image
        dialogueText.text = "";

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeSentence(line.dialogueLine));
    }

    IEnumerator TypeSentence(string sentence)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        typingCoroutine = null;
    }

    private void EndCutscene()
    {
        dialoguePanel.SetActive(false);
        currentCutscene = null;
        currentDialogueIndex = 0;
        // Potentially trigger an event or callback for cutscene completion
    }

    public bool IsCutsceneActive()
    {
        return dialoguePanel.activeSelf;
    }
}

[System.Serializable]
public class DialogueLine
{
    public string characterName;
    public string dialogueLine;
    public Sprite characterImage; // Image for the character or scene
}

[CreateAssetMenu(fileName = "NewCutscene", menuName = "Cutscene/Cutscene")]
public class Cutscene : ScriptableObject
{
    public DialogueLine[] dialogues;
}