using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum DialogueType
{
    Direct = 0,
    ColliderEnter = 1,
    ColliderExit = 2,
    Interact = 3,
}
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] public DialogueType dialogueType;
    public Dialogue dialogue;

    public void DirectTriggerDialogue()
    {
        GameManager.Instance.ChangeState(GameState.StartDialogue);
        DialogueSystem.Instance.StartDialogue(dialogue);
    }

    public void ColliderEnterTriggerDialogue()
    {
        if (dialogueType != DialogueType.ColliderEnter) return;
        DirectTriggerDialogue();
    }
    
    public void ColliderExitTriggerDialogue()
    {
        if (dialogueType != DialogueType.ColliderExit) return;
        DirectTriggerDialogue();
    }
    public void InteractTriggerDialogue()
    {
        if (dialogueType != DialogueType.Interact) return;
        DirectTriggerDialogue();
    }

    
    
    
    private void OnTriggerEnter(Collider other)
    {
        ColliderEnterTriggerDialogue();
    }

    private void OnTriggerExit(Collider other)
    {
        ColliderExitTriggerDialogue();
    }
}