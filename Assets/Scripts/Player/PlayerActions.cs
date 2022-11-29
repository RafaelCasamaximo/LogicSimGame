using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Script atrelado as ações que o player pode tomar enquanto FreeState e no InventorySystem
/// </summary>
public class PlayerActions : MonoBehaviour
{
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    
    /*
     * Handlers do Movement InputMap
     * Convenção de nome: Handle<Nome do Input Action><Nome do Mapping><Nome da Action>
     */
    public void HandlePlayerMovementOpenInventory(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        GameManager.Instance.ChangeState(GameState.OpenInventory);
        playerInput.SwitchCurrentActionMap("Inventory");  
    }
    
    /*
     * Handlers do Dialogue InputMap
     * Convenção de nome: Handle<Nome do Input Action><Nome do Mapping><Nome da Action>
     */
    public void HandlePlayerDialogueNextSentence(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        DialogueSystem.Instance.DisplayNextSentence();
    }
    
    public void HandlePlayerDialogueClose(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        GameManager.Instance.ChangeState(GameState.FreeGameplay);
        playerInput.SwitchCurrentActionMap("Movement");
        DialogueSystem.Instance.EndDialogue();
    }
    
    /*
     * Handlers do Inventory InputMap
     */
    public void HandlePlayerInventoryClose(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        GameManager.Instance.ChangeState(GameState.FreeGameplay);
        playerInput.SwitchCurrentActionMap("Movement");
    }
}
