using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorGate : Gate
{
    
    public bool state;

    public GeneratorGate(bool value)
    {
        state = value;
        SetOutput(state);
    }
    
    public void SetValue(bool value)
    {
        state = value;
        SetOutput(state);
    }
    
    public override void AddInput(Gate gate)
    {
        inputs.Add(gate);
        gate.OutputChanged += OnInputChanged;
        OnInputChanged();
    }

    
    protected override bool Execute()
    {
        return state;
    }
}
