using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Representa um Durable Item, que não pode ser consumível e deve ser utilizado em conjunto com outro interactee
/// Exemplo: Uma chave de fenda que não some do inventário depois de ser utilizada, mas precisa ser utilizada em um parafuso
/// </summary>
public class TestCube2Controller : MonoBehaviour, IDurable
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
        Debug.Log("TestCube2Controller - Durable Item");
    }
}
