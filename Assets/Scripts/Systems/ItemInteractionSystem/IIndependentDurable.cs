using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIndependentDurable
{
    public void HandleUseItem()
    {
        Debug.Log("Interface Log");
    }
}
