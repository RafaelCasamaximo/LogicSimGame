using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CircuitSimulatorPlacingCircuits : MonoBehaviour
{
    
    private Camera _camera;
    private int backgroundWidth;
    private int backgroundHeight;
    private Vector2 cursorMovement;
    [HideInInspector] public bool isMoving;
    [HideInInspector] public Vector3 origPos;
    [HideInInspector] public Vector3 targetPos;
    [HideInInspector] public Vector3Int boundary;
    [HideInInspector] public Vector3 globalLBBoundary;
    [HideInInspector] public Vector3 globalRTBoundary;

    [Header("Movement Settings")]
    public float timeToMove;
    public AudioClip[] movementSounds;
    
    [Header("Placing Settings")]
    public AudioClip confirmSound;
    public AudioClip cancelSound;
    public AudioClip errorSound;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        backgroundWidth = CircuitSimulatorManager.Instance.backgroundWidth;
        backgroundHeight = CircuitSimulatorManager.Instance.backgroundHeight;
        boundary = new Vector3Int(backgroundWidth - 1, backgroundHeight - 1, 0);
        globalLBBoundary = CircuitSimulatorManager.Instance.grid.GetCellCenterWorld(Vector3Int.zero);
        globalRTBoundary = CircuitSimulatorManager.Instance.grid.GetCellCenterWorld(boundary);
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
    public void HandleCircuitSimulatorPlacingCircuitsMove(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            cursorMovement = Vector2.zero;
            return;
        }
        if (!context.performed) return;
        cursorMovement = context.ReadValue<Vector2>();
    }

    public void HandleCircuitSimulatorPlacingCircuitConfirm(InputAction.CallbackContext context)
    {
        //TODO: Implementar instanciar o novo logic gate
    }
    
    public void HandleCircuitSimulatorPlacingCircuitCancel(InputAction.CallbackContext context)
    {
        //TODO: Implementar sair do modo placingCircuits
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
        transform.position = CircuitSimulatorManager.Instance.grid.GetCellCenterWorld(cellPosition);

        isMoving = false;
    }
    
}
