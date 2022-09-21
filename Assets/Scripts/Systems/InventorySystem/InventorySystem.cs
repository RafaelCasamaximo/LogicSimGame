using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : SingletonPersistent<InventorySystem>
{
    private Dictionary<InventoryItemData, InventoryItem> _itemDict;
    public List<InventoryItem> inventory { get; private set; }
    public int maxInventoryItems = 28;
    public InventoryItem selectedItem = null;
    public event Action onInventoryChangedEvent;
    public event Action onInventorySelectItem;
    public event Action onInventoryRemoveSelectedItem;

    protected override void Awake()
    {
        inventory = new List<InventoryItem>();
        _itemDict = new Dictionary<InventoryItemData, InventoryItem>();
        base.Awake();
    }

    public void Add(InventoryItemData referenceData)
    {

        if (_itemDict.Count >= maxInventoryItems)
        {
            throw new Exception("Inventory is full");
        }
        
        if (_itemDict.TryGetValue(referenceData, out InventoryItem value))
        {
            value.AddToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(referenceData);
            inventory.Add(newItem);
            _itemDict.Add(referenceData, newItem);
        }
        onInventoryChangedEvent?.Invoke();
        if (selectedItem == null) return;
        if (referenceData.id == selectedItem.data.id)
        {
            onInventorySelectItem?.Invoke();
        }
    }

    public void Remove(InventoryItemData referenceData)
    {
        if (_itemDict.TryGetValue(referenceData, out InventoryItem value))
        {
            value.RemoveFromStack();
            if (value.stackSize == 0)
            {
                inventory.Remove(value);
                _itemDict.Remove(referenceData);
            }
        }
        onInventoryChangedEvent?.Invoke();
        if (selectedItem == null) return;
        if (referenceData.id != selectedItem.data.id) return;
        if (selectedItem.stackSize <= 0)
        {
            selectedItem = null;
        }
        onInventoryRemoveSelectedItem?.Invoke();
    }

    public InventoryItem Get(InventoryItemData referenceData)
    {
        return _itemDict.TryGetValue(referenceData, out InventoryItem value) ? value : null;
    }

    public void SelectItem(InventoryItem referenceData)
    {
        selectedItem = referenceData;
        onInventorySelectItem?.Invoke();
    }
}
 