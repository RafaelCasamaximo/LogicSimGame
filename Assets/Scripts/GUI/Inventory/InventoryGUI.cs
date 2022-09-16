using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventoryGUI : GUIControl
{
    public override string key { get { return "InventoryGUI"; } }
    public GameObject grid;
    public GameObject gridElementPrefab;
    public PlayerInput playerInput;
    protected override void Start()
    {
        InventorySystem.Instance.onInventoryChangedEvent += OnUpdateInventory;
        base.Start();
    }

    public void OnUpdateInventory()
    {
        foreach (Transform t in grid.transform)
        {
            Destroy(t.gameObject);
        }
        DrawInventory();
    }
    
    public void DrawInventory()
    {
        foreach (InventoryItem item in InventorySystem.Instance.inventory)
        {
            AddGridElement(item);
        }
    }

    public void AddGridElement(InventoryItem item)
    {
        GameObject gridElementObj = Instantiate(gridElementPrefab, grid.transform);
        GridElement gridElement = gridElementObj.GetComponent<GridElement>();
        gridElement.Set(item);
    }
    
    
    /*
     * Handlers do Inventory InputMap
     */
    public void HandlePlayerInventoryClose(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        GameManager.Instance.ChangeState(GameState.FreeGameplay);
        playerInput.SwitchCurrentActionMap("Movement");
    }
}
