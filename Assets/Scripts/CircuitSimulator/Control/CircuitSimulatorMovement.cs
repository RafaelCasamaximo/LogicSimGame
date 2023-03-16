using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CircuitSimulatorMovement : MonoBehaviour
{

    private int backgroundWidth;
    private int backgroundHeight;
    private Vector3Int cursorStartPosition;
    private Vector3Int cursorCurrentPosition;
    private Vector2 cursorMovement;
    public int speedInterval;
    private int frameCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        frameCounter = 0;
        backgroundWidth = CircuitSimulatorManager.Instance.backgroundWidth;
        backgroundHeight = CircuitSimulatorManager.Instance.backgroundHeight;
        cursorStartPosition = new Vector3Int(backgroundWidth /2, backgroundHeight /2);
        cursorCurrentPosition = cursorStartPosition;
        CircuitSimulatorManager.Instance.circuitSimulatorRenderer.cursorLastPosition = cursorStartPosition;
        CircuitSimulatorManager.Instance.circuitSimulatorRenderer.ChangeCursorPosition(cursorStartPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (cursorMovement != Vector2.zero && frameCounter == 0)
        {
            cursorCurrentPosition += new Vector3Int((int)cursorMovement.x, (int)cursorMovement.y);
            CircuitSimulatorManager.Instance.circuitSimulatorRenderer.ChangeCursorPosition(cursorCurrentPosition);
        }
    }
    
    /*
     * Handlers do Movement InputMap
     * Convenção de nome: Handle<Nome do Input Action><Nome do Mapping><Nome da Action>
     */
    public void HandleCircuitSimulatorMovementMove(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            cursorMovement = Vector2.zero;
            return;
        }
        if (!context.performed) return;
        cursorMovement = context.ReadValue<Vector2>();
    }
    
}
