using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOTGate : Gate
{

    // Override porque o NOTGate precisa funcionar mesmo com um único input.
    public override void AddInput(Gate gate)
    {
        inputs.Add(gate);
        gate.OutputChanged += OnInputChanged;
        OnInputChanged();
    }

    protected override bool Execute()
    {
        return !inputs[0].GetOutput();
    }
}
