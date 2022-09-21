using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
