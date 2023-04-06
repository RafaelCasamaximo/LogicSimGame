using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CircuitSimulatorPlacingCircuits : MonoBehaviour
{
    
    private Camera _camera;
    private int backgroundWidth;
    private int backgroundHeight;
    private Vector2 cursorMovement;
    [HideInInspector] public Vector3Int cellPosition;
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
    public bool canPlace;

    [HideInInspector] public GameObject prefab;
    [HideInInspector] public InventoryItem item;

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

    // Update is called once per frame
    void Update()
    {
        if (cursorMovement != Vector2.zero && !isMoving)
        {
            StartCoroutine(MoveCursor(cursorMovement));
        }
    }

    public void Setup(GameObject prefab, InventoryItem item)
    {
        this.prefab = prefab;
        this.item = item;
        canPlace = CheckPosition(cellPosition);
    }

    public bool CheckPosition(Vector3Int cursorPosition)
    {
        //TODO: Implementar se a posição de inserção é válida e retornar o resultado
        /*
         * Para isso, iterar sobre a lista levelGates e useGates
         * para cada gate, pegar size
         * Comparar com posição atual
         * Se houver intersecção define como false
         * Senão, define como verdadeiro
         */
        var currentPoints = SizeToCell(prefab.GetComponent<Gate>().properties.size, cellPosition);
        bool intersects = false;
        foreach (var gateGO in CircuitSimulatorManager.Instance.levelGates)
        {
            Gate gate = gateGO.GetComponent<Gate>();
            intersects = CheckRectangleIntersection(currentPoints, SizeToCell(gate.properties.size, gate.properties.gridPosition));
            if (intersects) return false;
        }
        foreach (var gateGO in CircuitSimulatorManager.Instance.userGates)
        {
            Gate gate = gateGO.GetComponent<Gate>();
            intersects = CheckRectangleIntersection(currentPoints, SizeToCell(gate.properties.size, gate.properties.gridPosition));
            if (intersects) return false;
        }
        return true;
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
        if (!context.performed) return;
        //TODO: Implementar instanciar o novo logic gate
        if (isMoving)
        {
            return;
        }
        if (!canPlace)
        {
            SoundManager.Instance.PlaySound(errorSound);
            return;
        }

        InstantiateGate();
        SoundManager.Instance.PlaySound(confirmSound);
        InventorySystem.Instance.Remove(item.data);
        QuitPlacingCircuits();
        
    }

    public void QuitPlacingCircuits()
    {
        CircuitSimulatorManager.Instance.ResetCursorSprite();
        GameManager.Instance.ChangeState(GameState.CircuitSimulatorMoving);
        CircuitSimulatorManager.Instance.circuitSimulatorPlayerInput.SwitchCurrentActionMap("Movement");
    }
    
    public void InstantiateGate()
    {
        var gateGO = Instantiate(prefab, CircuitSimulatorManager.Instance.logicGatesParent.transform);

        switch (item.data.id)
        {
            case "andItem":
                gateGO.GetComponent<ANDGate>().Initialize(cellPosition);
                break;
            case "nandItem":
                gateGO.GetComponent<NANDGate>().Initialize(cellPosition);
                break;
            case "orItem":
                gateGO.GetComponent<ORGate>().Initialize(cellPosition);
                break;
            case "norItem":
                gateGO.GetComponent<NORGate>().Initialize(cellPosition);
                break;
            case "xorItem":
                gateGO.GetComponent<XORGate>().Initialize(cellPosition);
                break;
            case "xnorItem":
                gateGO.GetComponent<XNORGate>().Initialize(cellPosition);
                break;
            case "notItem":
                gateGO.GetComponent<NOTGate>().Initialize(cellPosition);
                break;
        }
        
        CircuitSimulatorManager.Instance.userGates.Add(gateGO);
        
    }
    
    public void HandleCircuitSimulatorPlacingCircuitCancel(InputAction.CallbackContext context)
    {
        SoundManager.Instance.PlaySound(cancelSound);
        CircuitSimulatorManager.Instance.ResetCursorSprite();
        GameManager.Instance.ChangeState(GameState.CircuitSimulatorMoving);
        CircuitSimulatorManager.Instance.circuitSimulatorPlayerInput.SwitchCurrentActionMap("Movement");
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

        cellPosition = CircuitSimulatorManager.Instance.grid.WorldToCell(transform.position);

        if (CheckPosition(cellPosition) != canPlace)
        {
            canPlace = CheckPosition(cellPosition);
            CircuitSimulatorManager.Instance.UpdateCursorSpriteAvailability(canPlace);
        }
        
        transform.position = CircuitSimulatorManager.Instance.grid.GetCellCenterWorld(cellPosition);

        isMoving = false;
    }

    private Vector3Int[] SizeToCell(Vector3Int size, Vector3Int position)
    {
        Vector3Int[] points = new Vector3Int[4];
        points[0] = position;
        points[1] = position;
        points[2] = position;
        points[3] = position;
        
        if (size.x != 3 || size.y != 3) return points;
        
        points[0].x = position.x - 1;
        points[0].y = position.y - 1;
        points[1].x = position.x - 1;
        points[1].y = position.y + 1;
        points[2].x = position.x + 1;
        points[2].y = position.y + 1;
        points[3].x = position.x + 1;
        points[3].y = position.y - 1;

        return points;
    }

    private bool CheckRectangleIntersection(Vector3Int[] r1, Vector3Int[] r2)
    {
        // Check if the two rectangles intersect by checking if their bounding boxes overlap
        int r1MinX = int.MaxValue, r1MaxX = int.MinValue, r1MinY = int.MaxValue, r1MaxY = int.MinValue;
        int r2MinX = int.MaxValue, r2MaxX = int.MinValue, r2MinY = int.MaxValue, r2MaxY = int.MinValue;

        // Find the minimum and maximum X and Y values for each rectangle
        for (int i = 0; i < 4; i++)
        {
            // Rectangle 1
            if (r1[i].x < r1MinX) r1MinX = r1[i].x;
            if (r1[i].x > r1MaxX) r1MaxX = r1[i].x;
            if (r1[i].y < r1MinY) r1MinY = r1[i].y;
            if (r1[i].y > r1MaxY) r1MaxY = r1[i].y;

            // Rectangle 2
            if (r2[i].x < r2MinX) r2MinX = r2[i].x;
            if (r2[i].x > r2MaxX) r2MaxX = r2[i].x;
            if (r2[i].y < r2MinY) r2MinY = r2[i].y;
            if (r2[i].y > r2MaxY) r2MaxY = r2[i].y;
        }

        // Check if the bounding boxes overlap
        bool xOverlap = (r1MinX <= r2MaxX) && (r1MaxX >= r2MinX);
        bool yOverlap = (r1MinY <= r2MaxY) && (r1MaxY >= r2MinY);

        // Return true if there is an overlap in both the X and Y axes
        return xOverlap && yOverlap;
    }


    
}
