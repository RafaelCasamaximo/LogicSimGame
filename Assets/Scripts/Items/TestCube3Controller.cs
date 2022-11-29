using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Representa um Independent Durable Item, que não pode ser consumível e e funciona sozinho
/// Exemplo: Uma lanterna que pode ser utilizada sem necessitar de nenhum outro item. 
/// </summary>
public class TestCube3Controller : MonoBehaviour, IIndependentDurable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleUseItem()
    {
        Debug.Log("TestCube3Controller - Independent Durable Item");
    }
}
