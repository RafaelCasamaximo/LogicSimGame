using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface que lida com os interactees e os sucessos e falhas
/// </summary>
public interface IInteractee
{
    public void HandleItemInteraction(ObjectType objectType);
    public void InteractionSuccess();
    public void InteractionFailed();
}
