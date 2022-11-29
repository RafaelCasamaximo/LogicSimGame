using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ITEM DE TESTE
/// Essa classe representa um interactee de teste no qual o jogador pode interagir E NÃO CONSOME O ITEM DO REQUIREMENT
/// A interação depende de um requirement no inventario do player
/// As funções  InteractionSuccess ou InteractionFailed representam os estados resultantes da função HandleItemInteraction.
/// </summary>
public class PrototypeInteractee : MonoBehaviour, IInteractee
{
    [SerializeField] private Requirement requirement;

    public void HandleItemInteraction(ObjectType objectType)
    {
        if (requirement.CheckRequirements())
        {
            if (objectType == ObjectType.Consumable)
            {
                requirement.RemoveRequirementsFromInventory();
            }
            InteractionSuccess();
        }
        else
        {
            InteractionFailed();
        }
    }

    public void InteractionSuccess()
    {
        SoundManager.Instance.PlaySound(requirement.successClip);
        LevelManager.Instance.LoadScene("CircuitSimulator", GameState.Start);
    }

    public void InteractionFailed()
    {
        SoundManager.Instance.PlaySound(requirement.failureClip);
        Debug.Log("You don't meet the requirements: " + requirement);
    }
}
