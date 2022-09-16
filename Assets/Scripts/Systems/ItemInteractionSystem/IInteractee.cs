using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractee
{
    public void HandleItemInteraction();
    public void InteractionSuccess();
    public void InteractionFailed();
}
