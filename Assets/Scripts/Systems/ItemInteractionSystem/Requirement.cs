using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Requirement")]
public class Requirement : ScriptableObject
{
    public InventoryItemData requiredSelectedItem;
    public RequirementItem[] requirementItems;
    public AudioClip successClip;
    public AudioClip failureClip;

    public bool CheckRequirements()
    {
        bool isSatisfied = true;

        if (requiredSelectedItem.id != InventorySystem.Instance.selectedItem.data.id)
        {
            isSatisfied = false;
            return isSatisfied;
        }
        
        foreach (RequirementItem requirementItem in requirementItems)
        {
            InventoryItem inventoryItem = InventorySystem.Instance.Get(requirementItem.inventoryItemData);
            if (inventoryItem is null)
            {
                isSatisfied = false;
                break;
            }
            else if (inventoryItem.stackSize < requirementItem.quantity)
            {
                isSatisfied = false;
                break;
            }
        }

        return isSatisfied;
    }

    public void RemoveRequirementsFromInventory()
    {
        foreach (RequirementItem requirementItem in requirementItems)
        {
            InventorySystem.Instance.Remove(requirementItem.inventoryItemData);
        }
    }
}

[System.Serializable]
public struct RequirementItem
{
    public InventoryItemData inventoryItemData;
    public int quantity;
}