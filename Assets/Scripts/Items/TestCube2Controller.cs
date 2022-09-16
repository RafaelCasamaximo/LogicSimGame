using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
