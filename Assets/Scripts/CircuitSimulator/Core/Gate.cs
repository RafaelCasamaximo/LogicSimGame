using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gate
{
    public List<Gate> inputs = new List<Gate>();
    public event Action OutputChanged;
    private bool output;

    public virtual void AddInput(Gate gate)
    {
        inputs.Add(gate);
        // OutputChanged += gate.OnInputChanged;
        gate.OutputChanged += OnInputChanged;
        
        // Essa verificação existe para que o método não tente ser acionado logo após a inserção do 1o input.
        // Ele é virtual pois o NOT precisa que ele execute somente com 1 input. Logo, ele é overrided.
        if (inputs.Count > 1)
        {
            OnInputChanged();
        }
        
    }
    
    public void SetInput(int index, Gate gate)
    {
        if (index >= 0 && index < inputs.Count) {
            // OutputChanged -= inputs[index].OnInputChanged;
            // inputs[index] = gate;
            // OutputChanged += inputs[index].OnInputChanged;

            inputs[index].OutputChanged -= OnInputChanged;
            inputs[index] = gate;
            inputs[index].OutputChanged += OnInputChanged;
            OnInputChanged();
        } else {
            throw new IndexOutOfRangeException("Invalid input index");
        }
    }

    public bool GetOutput()
    {
        return output;
    }

    public void SetOutput(bool value)
    {
        output = value;
        OutputChanged?.Invoke();
    }

    public void OnInputChanged()
    {
        output = Execute();
        OutputChanged?.Invoke();
    }
    
    protected abstract bool Execute();
    
}
