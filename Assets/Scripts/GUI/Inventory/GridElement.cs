using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Elemento de interface auxiliar que representa um grid que contém e mostra um item no inventário.
/// É instanciado pelo inventoryGUI e o conteúdo é gerenciado por inventoryGUI e o InventoryManager
/// Os parâmetros são os mesmos do scriptableObject do item.
/// Esse script é atrelado ao prefab do placeholder do item.
/// </summary>
public class GridElement : MonoBehaviour
{

    [SerializeField] public InventoryItem inventoryItem;
    [SerializeField] private TextMeshProUGUI displayName;
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
    
    /// <summary>
    /// Método invocado quando o jogador seleciona o item em questão.
    /// O método SelectItem é responsável por renderizar o item na mão do jogador e acertar os pormenores
    /// O state do jogo é alterado para FreeGameplay depois para continuar o ciclo.
    /// </summary>
    private void HandleSelectItem()
    {
        InventorySystem.Instance.SelectItem(inventoryItem);
        GameManager.Instance.ChangeState(GameState.FreeGameplay);
        InventorySystem.Instance.playerInput.SwitchCurrentActionMap("Movement");
    }
}
