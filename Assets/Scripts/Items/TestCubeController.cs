using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Representa um Consumable Item, que pode ser consumível ao ser interagido com um interactee que possua um requirement que contenha o item.
/// Exemplo: Uma chave que precisa ser inserida em uma porta. Após a chave ser inserida, ela some do inventario do player, e necessita da porta para a interação
/// </summary>
public class TestCubeController : MonoBehaviour, IConsumable
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
        Debug.Log("TestCubeController - Consumable Item");
        //InventorySystem.Instance.Remove(InventorySystem.Instance.selectedItem.data);
    }
}
