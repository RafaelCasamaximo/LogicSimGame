using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GridElement : MonoBehaviour
{

    [SerializeField] public InventoryItem inventoryItem;
    [SerializeField] private TextMeshProUGUI displayName;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI quantity;
    [SerializeField] private GameObject tooltip;
    [SerializeField] private Button button;


    public void Set(InventoryItem item)
    {
        inventoryItem = item;
        button.onClick.AddListener(delegate { HandleSelectItem(); });
        displayName.text = item.data.displayName;
        icon.sprite = item.data.icon;
        tooltip.SetActive(true);
        if (item.stackSize <= 1)
        {
            tooltip.SetActive(false);
            return;
        }
        quantity.text = item.stackSize.ToString();
    }
    
    private void HandleSelectItem()
    {
        InventorySystem.Instance.SelectItem(inventoryItem);
        GameManager.Instance.ChangeState(GameState.FreeGameplay);
    }
}
