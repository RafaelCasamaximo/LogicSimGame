using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PersistentGUI : GUIControl
{
    public override string key { get { return "PersistentGUI"; } }
    public PersistentItemElement persistentItemElement = null;

    protected override void Start()
    {
        DrawSelectItem();
        InventorySystem.Instance.onInventorySelectItem += DrawSelectItem;
        InventorySystem.Instance.onInventoryRemoveSelectedItem += DrawSelectItem;
        base.Start();
    }


    public void DrawSelectItem()
    {
        persistentItemElement.Set(InventorySystem.Instance.selectedItem);
    }
}
