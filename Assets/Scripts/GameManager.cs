using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
    // Start
    Start = 0,
    
    // 1st Person Gameplay
    FreeGameplay = 1,
    OpenInventory = 2,
    StartDialogue = 3,
    
    // Circuit Simulator
    CircuitSimulator = 4,
    CircuitSimulatorFreeCamera = 5,
    CircuitSimulatorMoving = 6,
    CircuitSimulatorInventory = 7,
    CircuitSimulatorPlacingCircuits = 8,
    CircuitSimulatorPlacingWires = 9,
    CircuitSimulatorComparingOutput = 10,
}


public class GameManager : Singleton<GameManager>
{
    public GameState State { get; private set; }
    public GameObject firstPersonPlayer;
    public GameObject circuitSimulatorPlayer;
    [NonSerialized] public PlayerInput playerInput;
    [NonSerialized] public PlayerInput circuitSimulatorPlayerInput;
    
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
            case GameState.CircuitSimulator:
                HandleCircuitSimulator();
                break;
            case GameState.CircuitSimulatorFreeCamera:
                HandleCircuitSimulatorFreeCamera();
                break;
            case GameState.CircuitSimulatorMoving:
                HandleCircuitSimulatorMoving();
                break;
            case GameState.CircuitSimulatorInventory:
                HandleCircuitSimulatorInventory();
                break;
            case GameState.CircuitSimulatorPlacingCircuits:
                HandleCircuitSimulatorPlacingCircuits();
                break;
            case GameState.CircuitSimulatorPlacingWires:
                HandleCircuitSimulatorPlacingWires();
                break;
            case GameState.CircuitSimulatorComparingOutput:
                HandleCircuitSimulatorComparingOutput();
                break;
        }

        // TODO: Aprender a utilizar o sistema de events para poder adicionar OnAfterStateChanged aqui 
    }

    public void ChangeSceneMiddleware(GameState afterSceneChange)
    {
        //GUIManager.Instance.UnregisterAll();
        ChangeState(afterSceneChange);
    }


    private void HandleStart()
    {
        // Faz as configurações iniciais da cena
        GUIManager.Instance.HideAll();

        switch (SceneManager.GetActiveScene().name)
        {
            case "TestPlayeground":
                Debug.Log("Entrando em TestPlayeground");
                var firstPersonPlayerInstance = Instantiate(firstPersonPlayer);
                playerInput = firstPersonPlayerInstance.GetComponent<PlayerInput>();
                DialogueSystem.Instance.playerInput = playerInput;
                ChangeState(GameState.FreeGameplay);
                break;
            case "CircuitSimulator":
                Debug.Log("Entrando em CircuitSimulator");
                var circuitSimulatorPlayerInstance = Instantiate(circuitSimulatorPlayer);
                circuitSimulatorPlayerInput = circuitSimulatorPlayerInstance.GetComponent<PlayerInput>();
                ChangeState(GameState.CircuitSimulator);
                break;
            case "MainHub":
                Debug.Log("Entrando em MainHub");
                var firstPersonPlayerMainHubInstance = Instantiate(firstPersonPlayer);
                playerInput = firstPersonPlayerMainHubInstance.GetComponent<PlayerInput>();
                DialogueSystem.Instance.playerInput = playerInput;
                ChangeState(GameState.FreeGameplay);
                break;
        }
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

    private void HandleCircuitSimulator()
    {
        GameManagerUtilities.LockMouse();
        GUIManager.Instance.HideAll();
        GUIManager.Instance.Show("CircuitSimulatorPersistentGUI");
        ChangeState(GameState.CircuitSimulatorMoving);
    }

    private void HandleCircuitSimulatorFreeCamera()
    {
        circuitSimulatorPlayerInput.SwitchCurrentActionMap("FreeCamera");
    }
    
    private void HandleCircuitSimulatorMoving()
    {
        circuitSimulatorPlayerInput.SwitchCurrentActionMap("Movement");
    }
    private void HandleCircuitSimulatorInventory()
    {
        circuitSimulatorPlayerInput.SwitchCurrentActionMap("Inventory");
    }

    private void HandleCircuitSimulatorPlacingCircuits()
    {
        circuitSimulatorPlayerInput.SwitchCurrentActionMap("PlacingCircuits");
    }

    private void HandleCircuitSimulatorPlacingWires()
    {
        circuitSimulatorPlayerInput.SwitchCurrentActionMap("PlacingWires");
    }

    private void HandleCircuitSimulatorComparingOutput()
    {
        circuitSimulatorPlayerInput.SwitchCurrentActionMap("ComparingOutput");
    }


}
