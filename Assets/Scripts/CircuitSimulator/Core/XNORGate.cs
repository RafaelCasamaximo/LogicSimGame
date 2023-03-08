using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XNORGate : Gate
{
    protected override bool Execute()
    {
        return !(inputs[0].GetOutput() ^ inputs[1].GetOutput());
    }
}
