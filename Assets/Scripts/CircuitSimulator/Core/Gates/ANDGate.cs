using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANDGate : Gate
{
    // Start is called before the first frame update
    public override bool Execute()
    {
        bool value1 = input1.hasGate & input1.state;
        bool value2 = input2.hasGate & input2.state;

        Debug.Log(this + ": " + "(" + value1 + " AND " + value2 + "): " + (value1 & value2));
        
        return value1 & value2;
    }
}
