using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class DialogueSystem : Singleton<DialogueSystem>
{
    
    // public Text 
    public DialogueGUI dialogueGUI;
    public Queue<string> sentences;
    public PlayerInput playerInput;

    void Start()
    {
        sentences = new Queue<string>();
    }
    
    public void StartDialogue(Dialogue dialogue)
    {
        playerInput.SwitchCurrentActionMap("Dialogue");
        dialogueGUI.SetSpeaker(dialogue.name);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueGUI.SetSentence(sentence);
    }

    public void EndDialogue()
    {
        sentences.Clear();
        dialogueGUI.SetSentence("<Dialogue Sentence>");
        dialogueGUI.SetSpeaker("<Speaker>");
        GameManager.Instance.ChangeState(GameState.FreeGameplay);
        playerInput.SwitchCurrentActionMap("Movement");
    }
    
}
