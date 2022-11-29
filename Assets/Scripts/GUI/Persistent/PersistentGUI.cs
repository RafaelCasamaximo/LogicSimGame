using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Essa classe é atrelada ao canvas que representa a UI que é constante durante o estado de FreeGameplay e em outros momentos (como mira, objetos na mão, e etc)
/// </summary>
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

    /// <summary>
    /// Desenha o objeto selecionado pelo InventorySystem na mão do player
    /// </summary>
    public void DrawSelectItem()
    {
        persistentItemElement.Set(InventorySystem.Instance.selectedItem);
    }
}
