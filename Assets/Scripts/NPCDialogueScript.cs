using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [TextArea(2, 5)]
    public string[] dialogueLines;

    public void Interact()
    {
        
        if (!DialogueManager.Instance.IsDialogueActive)
        {
            Debug.Log("NPC.Interact() wurde ausgel�st!");
            DialogueManager.Instance.StartDialogue(dialogueLines);
        }
    }
}
