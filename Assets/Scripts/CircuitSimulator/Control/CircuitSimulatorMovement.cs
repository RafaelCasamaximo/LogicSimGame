using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CircuitSimulatorMovement : MonoBehaviour
{
    private Camera _camera;
    private int backgroundWidth;
    private int backgroundHeight;
    private float horizontalMin;
    private float horizontalMax;
    private float verticalMin;
    private float verticalMax;
    private Vector2 cursorMovement;
    public Vector3 origPos;
    public Vector3 targetPos;
    public float timeToMove;
    public AudioClip[] movementSounds;
    public Vector3Int boundary;
    public Vector3 globalLBBoundary;
    public Vector3 globalRTBoundary;

    public bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        backgroundWidth = CircuitSimulatorManager.Instance.backgroundWidth;
        backgroundHeight = CircuitSimulatorManager.Instance.backgroundHeight;
        boundary = new Vector3Int(backgroundWidth - 1, backgroundHeight - 1, 0);
        globalLBBoundary = CircuitSimulatorManager.Instance.grid.GetCellCenterWorld(Vector3Int.zero);
        globalRTBoundary = CircuitSimulatorManager.Instance.grid.GetCellCenterWorld(boundary);

        float halfHeight = _camera.orthographicSize;
        float halfWidth = _camera.aspect * halfHeight;

        horizontalMin = halfWidth;
        horizontalMax = - halfWidth + backgroundWidth;
        verticalMin = halfHeight;
        verticalMax = - halfHeight + backgroundHeight;

        transform.parent.position = new Vector3((horizontalMin + horizontalMax) / 2, (verticalMin + verticalMax) / 2, -10f);
        SoundManager.Instance.ChangeEffectsVolume(0.1f);
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

    /*
     * Handlers do Movement InputMap
     * Convenção de nome: Handle<Nome do Input Action><Nome do Mapping><Nome da Action>
     */
    public void HandleCircuitSimulatorMovementOpenInventory(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        GameManager.Instance.ChangeState(GameState.CircuitSimulatorInventory);
        CircuitSimulatorManager.Instance.circuitSimulatorPlayerInput.SwitchCurrentActionMap("Inventory");
    }
    
    /*
     * Handlers do Movement InputMap
     * Convenção de nome: Handle<Nome do Input Action><Nome do Mapping><Nome da Action>
     */
    public void HandleCircuitSimulatorMovementExit(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
    }
    
    /*
     * Handlers do Movement InputMap
     * Convenção de nome: Handle<Nome do Input Action><Nome do Mapping><Nome da Action>
     */
    public void HandleCircuitSimulatorMovementCompareOutputs(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        GameManager.Instance.ChangeState(GameState.CircuitSimulatorComparingOutput);
        CircuitSimulatorManager.Instance.circuitSimulatorPlayerInput.SwitchCurrentActionMap("ComparingOutput");
    }
    
    /*
     * Handlers do Movement InputMap
     * Convenção de nome: Handle<Nome do Input Action><Nome do Mapping><Nome da Action>
     */
    public void HandleCircuitSimulatorMovementResetCircuit(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
    }
    
    
    public IEnumerator MoveCursor(Vector3 direction)
    {
        isMoving = true;
        SoundManager.Instance.PlaySound(movementSounds[Random.Range(0, movementSounds.Length - 1)]);

        float elapsedTime = 0f;

        origPos = transform.position;
        targetPos = origPos + direction;
        targetPos.x = Mathf.Clamp(targetPos.x, globalLBBoundary.x, globalRTBoundary.x);
        targetPos.y = Mathf.Clamp(targetPos.y, globalLBBoundary.y, globalRTBoundary.y);

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Vector3Int cellPosition = CircuitSimulatorManager.Instance.grid.WorldToCell(transform.position);
        // cellPosition.Clamp(Vector3Int.zero, boundary);
        transform.position = CircuitSimulatorManager.Instance.grid.GetCellCenterWorld(cellPosition);

        isMoving = false;
    }
    
}
