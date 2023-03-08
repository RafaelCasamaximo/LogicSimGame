using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaderGate : Gate
{
    
    public string label;
    
    public override void AddInput(Gate gate)
    {
        inputs.Add(gate);
        gate.OutputChanged += OnInputChanged;
        OnInputChanged();
    }

    protected override bool Execute()
    {
        return inputs[0].GetOutput();
    }
}
