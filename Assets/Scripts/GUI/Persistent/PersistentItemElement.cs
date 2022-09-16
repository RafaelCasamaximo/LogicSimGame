using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PersistentItemElement : MonoBehaviour
{
    
    [SerializeField] public InventoryItem inventoryItem;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI quantity;
    [SerializeField] private GameObject tooltip;


    public void Set(InventoryItem item)
    {
        if (item is null)
        {
            inventoryItem = null;
            icon.gameObject.SetActive(false);
            tooltip.SetActive(false);
            return;
        }
        
        inventoryItem = item;
        icon.sprite = item.data.icon;
        icon.gameObject.SetActive(true);
        tooltip.SetActive(true);
        if (item.stackSize <= 1)
        {
            tooltip.SetActive(false);
            return;
        }
        quantity.text = item.stackSize.ToString();
    }
}
