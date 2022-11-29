using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface que lida com os itens independentes e duráveis
/// </summary>
public interface IIndependentDurable
{
    public void HandleUseItem()
    {
        Debug.Log("Interface Log");
    }
}
