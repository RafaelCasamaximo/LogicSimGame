using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractee
{
    public void HandleItemInteraction(ObjectType objectType);
    public void InteractionSuccess();
    public void InteractionFailed();
}
