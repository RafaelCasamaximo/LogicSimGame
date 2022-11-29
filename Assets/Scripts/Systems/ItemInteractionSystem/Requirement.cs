using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Scriptable Object que é uma lista de requerimento para a interação com algum objeto.
/// Exemplo: A porta só pode abrir se o jogador tiver a chave certa. Logo, uma instancia desse SO é criada contendo a chave que deve ser utilizada, qual item o jogador deve possuir na mão, e os sons de sucesso e falha.
/// Permite adicionar varios itens nos requerimentos como por exemplo: uma carta que precisa de 5 selos diferentes e requer que o jogador tenha o envelope em mãos ao interagir com a caixa de correio.
/// </summary>
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