using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CircuitSimulatorInventory : MonoBehaviour
{

    /*
     * Handlers do Movement InputMap
     * Convenção de nome: Handle<Nome do Input Action><Nome do Mapping><Nome da Action>
     */
    public void HandleCircuitSimulatorInventoryCloseInventory(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        GameManager.Instance.ChangeState(GameState.CircuitSimulator);
        GUIManager.Instance.Hide("CircuitSimulatorInventoryGUI");
    }
}
