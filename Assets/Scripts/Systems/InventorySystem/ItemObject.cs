using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Encapsulamento do item que pode ser pegado pelo jogador.
/// </summary>
public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;
    
    public void HandlePickupItem()
    {
        InventorySystem.Instance.Add(referenceItem);
        SoundManager.Instance.PlaySound(referenceItem.pickupSound);
        Destroy(gameObject);
    }
}
