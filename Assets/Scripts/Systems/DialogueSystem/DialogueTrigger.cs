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

/// <summary>
/// O DialogueTrigger é o responsável por iniciar o DialogueSystem e fazer com que ele mostre a interface.
/// Esse script deve ser atrelado aos objetos que terão algum dialogo relacionado a eles.
/// Os 4 modos de dialogos são:
/// Direto: pode ser invocado através de programação
/// ColliderEnter: o dialogo inicia ao entrar numa área de um collider com isTrigger ativado
/// ColliderExit: o dialogo inicia ao sair numa área de um collider com isTrigger ativado
/// Interact: O dialogo inicia após o jogador interagir com o objeto que contém o script atrelado
/// O modo pode ser configurado no inspector
/// </summary>
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] public DialogueType dialogueType;
    [SerializeField] public Dialogue dialogue;

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