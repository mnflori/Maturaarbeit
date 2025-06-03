using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialogueBox;
    public Text dialogueText;

    public bool IsDialogueActive { get; private set; } = false;

    private string[] lines;
    private int currentLine = 0;
    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Update()
    {
        if(IsDialogueActive && Keyboard.current.eKey.wasPressedThisFrame)
        {
            AdvanceDialogue();
        }
    }

    public void StartDialogue(string[] dialogueLines)
    {
        
        dialogueBox.SetActive(true);
        lines = dialogueLines;
        currentLine = 0;
        IsDialogueActive = true;
        
 
        dialogueText.text = lines[currentLine];

       
        
    }
    
    private void AdvanceDialogue()
    {
        
        currentLine++;

        if (currentLine < lines.Length)
        {
            
            dialogueText.text = lines[currentLine];

            
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
        {
        dialogueBox.SetActive(false);
        IsDialogueActive = false;
        
        }
    
}
