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
        WireRenderer wire = gameObject.AddComponent<WireRenderer>();
        wire.Initialize(gate.outputLocation, inputLocations[inputs.Count - 1]);
        gate.outputWires.Add(wire);
        OnInputChanged();
    }

    protected override bool Execute()
    {
        return inputs[0].GetOutput();
    }
    
    public override void Initialize(Vector3Int gridPosition)
    {
        position = gridPosition;
        outputWires = new List<WireRenderer>();
        size = new Vector2Int(0 + position.x, 0 + position.y);
        inputLocations.Add(new Vector2Int(0 + gridPosition.x, 0 + gridPosition.y));
        gateTileBase = CircuitSimulatorManager.Instance.logicGatesTiles[(int)LogicGate.Reader];
        CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.SetTile(gridPosition, gateTileBase);
    }

    public void SetLabel(string value)
    {
        label = value;
    }
    
    public string GetLabel()
    {
        return label;
    }
}
