using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CircuitSimulatorPlacingWires : MonoBehaviour
{
    
    private Camera _camera;
    private int backgroundWidth;
    private int backgroundHeight;
    private Vector2 cursorMovement;
    [HideInInspector] public Vector3Int boundary;
    [HideInInspector] public Vector3 globalLBBoundary;
    [HideInInspector] public Vector3 globalRTBoundary;
    [HideInInspector] public Vector3Int cellPosition;
    
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
        cellPosition = CircuitSimulatorManager.Instance.grid.WorldToCell(transform.position);
    }

    public void Setup()
    {
        // Instancia um fio
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /*
     * Handlers do Movement InputMap
     * Convenção de nome: Handle<Nome do Input Action><Nome do Mapping><Nome da Action>
     */
    public void HandleCircuitSimulatorPlacingWiresMove(InputAction.CallbackContext context)
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
    public void HandleCircuitSimulatorPlacingWiresCancel(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        // Implementar método para deleção do fio
        // Tocar som
        // Sair do ActionMap e voltar para movement
    }
    
    /*
     * Handlers do Movement InputMap
     * Convenção de nome: Handle<Nome do Input Action><Nome do Mapping><Nome da Action>
     */
    public void HandleCircuitSimulatorPlacingWiresConfirm(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        // Verificar se é posição válida
    }
    
    // TODO: Implementar método MoveCursor que atualiza o fio
}
