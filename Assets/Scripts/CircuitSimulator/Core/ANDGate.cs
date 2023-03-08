using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANDGate : Gate
{
    
    
    protected override bool Execute()
    {
        Debug.Log("AND: " + inputs[0].GetOutput() + " AND " + inputs[1].GetOutput());
        return inputs[0].GetOutput() && inputs[1].GetOutput();
    }
}
