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
    public Vector3 origPos;
    public Vector3 targetPos;
    public float timeToMove;

    public bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        // frameCounter = 0;
        // backgroundWidth = CircuitSimulatorManager.Instance.backgroundWidth;
        // backgroundHeight = CircuitSimulatorManager.Instance.backgroundHeight;
        // cursorStartPosition = new Vector3Int(backgroundWidth /2, backgroundHeight /2);
        // cursorCurrentPosition = cursorStartPosition;
        // CircuitSimulatorManager.Instance.circuitSimulatorRenderer.cursorLastPosition = cursorStartPosition;
        // CircuitSimulatorManager.Instance.circuitSimulatorRenderer.ChangeCursorPosition(cursorStartPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (cursorMovement != Vector2.zero && !isMoving)
        {
            StartCoroutine(MoveCursor(cursorMovement));
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

    public IEnumerator MoveCursor(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0f;

        origPos = transform.position;
        targetPos = origPos + direction;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Vector3Int cellPosition = CircuitSimulatorManager.Instance.grid.WorldToCell(transform.position);
        transform.position = CircuitSimulatorManager.Instance.grid.GetCellCenterWorld(cellPosition);

        isMoving = false;
    }
    
    
}
