using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// O Dialogue System é um singleton que permite que um dialogo seja invocado em qualquer momento do jogo, desde que haja o DialogueGUI instanciado na cena
/// Ele altera e passa as sentenças do dialogo, inicia e fecha dialogos de acordo com as ações do player definida no InputController
/// O DialogueSystem é invocado pelo DialogueTrigger
/// </summary>
public class DialogueSystem : SingletonPersistent<DialogueSystem>
{
    
    // public Text 
    public DialogueGUI dialogueGUI;
    public Queue<string> sentences;
    public PlayerInput playerInput;

    void Start()
    {
        sentences = new Queue<string>();
        Debug.Log("");
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
