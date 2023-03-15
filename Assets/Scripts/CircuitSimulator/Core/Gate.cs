using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Gate : MonoBehaviour
{
    /// <summary>
    /// This variables are responsible for the
    /// gate logic. Not the graphic logic
    /// </summary>
    public List<Gate> inputs = new List<Gate>();
    public event Action OutputChanged;
    private bool output;

    /// <summary>
    /// This part is Responsible for the
    /// gate graphics.
    /// </summary>
    public Vector3Int position;
    public TileBase gateTileBase;
    public Vector2Int size;
    public List<Vector2Int> inputLocations = new List<Vector2Int>();
    public Vector2Int outputLocation;
    public List<WireRenderer> outputWires = new List<WireRenderer>();


    public virtual void AddInput(Gate gate)
    {
        inputs.Add(gate);
        gate.OutputChanged += OnInputChanged;
        WireRenderer wire = gameObject.AddComponent<WireRenderer>();
        wire.Initialize(gate.outputLocation, inputLocations[inputs.Count - 1]);
        gate.outputWires.Add(wire);

        // Essa verificação existe para que o método não tente ser acionado logo após a inserção do 1o input.
        // Ele é virtual pois o NOT precisa que ele execute somente com 1 input. Logo, ele é overrided.
        if (inputs.Count > 1)
        {
            OnInputChanged();
        }
        
    }
    
    public virtual void SetInput(int index, Gate gate)
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

    public virtual bool GetOutput()
    {
        return output;
    }

    public virtual void SetOutput(bool value)
    {
        output = value;
        OutputChanged?.Invoke();
    }

    public virtual void OnInputChanged()
    {
        output = Execute();
        OutputChanged?.Invoke();
    }
    
    protected abstract bool Execute();

    public abstract void Initialize(Vector3Int gridPosition);

}
