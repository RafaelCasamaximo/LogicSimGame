using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubDoorInterectee : MonoBehaviour, IInteractee
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
        Debug.Log("You met the requirements: " + requirement);
    }

    public void InteractionFailed()
    {
        SoundManager.Instance.PlaySound(requirement.failureClip);
        Debug.Log("You don't meet the requirements: " + requirement);
    }
}
