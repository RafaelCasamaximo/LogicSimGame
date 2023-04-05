using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CircuitSimulatorGridElement : MonoBehaviour
{
    
    [SerializeField] public InventoryItem inventoryItem;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI quantity;
    [SerializeField] private GameObject tooltip;
    [SerializeField] private Button button;
    
    /// <summary>
    /// Define os parâmetros no lugar do placeholder padrão.
    /// Também define um delegate para que quando o botão seja clicado o item invoque a função HandleSelectItem por meio de events e delegates.
    /// </summary>
    /// <param name="item">Wrap do Scriptable Object do Item</param>
    public void Set(InventoryItem item)
    {
        inventoryItem = item;
        // button.onClick.AddListener(delegate { HandleSelectItem(); });
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
        
    }
}
