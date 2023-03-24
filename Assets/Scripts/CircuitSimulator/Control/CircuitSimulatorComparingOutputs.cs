using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CircuitSimulatorComparingOutputs : MonoBehaviour
{
    public void HandleCircuitSimulatorComparingOutputsCloseComparingOutputs(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        GameManager.Instance.ChangeState(GameState.CircuitSimulator);
        CircuitSimulatorManager.Instance.circuitSimulatorPlayerInput.SwitchCurrentActionMap("Movement");
        // TODO: Fazer fechar a interface do Comparing Outputs aqui
        // GUIManager.Instance.Hide("CircuitSimulatorInventoryGUI");
    }
}
