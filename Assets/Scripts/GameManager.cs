using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

/// <summary>
/// Game manager é uma classe responsável por gerenciar os estados do jogo. É um singleton que
/// consegue transicionar para diferentes estados. A intenção é usar a classe para pode alterar
/// entre os diferentes modo do jogo, como por exemplo gameplay e interfaces, utilizando os mappings
/// do novo input system.
///
/// Para cada estado novo é necessário adicionar no enum e fazer um método Handle<NovoEstado>
/// adicionando sua invocação no switch case.
/// </summary>

[Serializable]
public enum GameState
{
    Start = 0,
    FreeGameplay = 1,
    OpenInventory = 2,
    StartDialogue = 3,
}


public class GameManager : Singleton<GameManager>
{
    public GameState State { get; private set; }
    public PlayerInput playerInput;

    private void Start() => ChangeState(GameState.Start);

    public void ChangeState(GameState newState)
    {
        // TODO: Aprender a utilizar o sistema de events para poder adicionar OnBeforeStateChanged aqui

        State = newState;
        switch (newState)
        {
            case GameState.Start:
                HandleStart();
                break;
            case GameState.FreeGameplay:
                HandleFreeGameplay();
                break;
            case GameState.OpenInventory:
                HandleOpenInventory();
                break;
            case GameState.StartDialogue:
                HandleStartDialogue();
                break;
        }

        // TODO: Aprender a utilizar o sistema de events para poder adicionar OnAfterStateChanged aqui 
    }

    private void HandleStart()
    {
        // Faz as configurações iniciais da cena
        GUIManager.Instance.HideAll();
        
        // Altera para o próximo estado
        ChangeState(GameState.FreeGameplay);
    }

    private void HandleFreeGameplay()
    {
        GameManagerUtilities.LockMouse();
        GUIManager.Instance.HideAll();
        GUIManager.Instance.Show("PersistentGUI");
        playerInput.SwitchCurrentActionMap("Movement");
    }
    
    private void HandleOpenInventory()
    {
        GameManagerUtilities.UnlockMouse();
        GUIManager.Instance.Show("InventoryGUI");
    }

    private void HandleStartDialogue()
    {
        
        GUIManager.Instance.Show("DialogueGUI");
    }


}
