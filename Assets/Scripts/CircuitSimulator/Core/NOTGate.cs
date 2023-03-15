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
    
    public override void Initialize(Vector3Int gridPosition)
    {
        outputWires = new List<WireRenderer>();
        size = new Vector2Int(1, 2);
        inputLocations.Add(new Vector2Int(1, 1));
        outputLocation = new Vector2Int(1, 2);
        gateTileBase = CircuitSimulatorManager.Instance.logicGatesTiles[(int)LogicGate.NOT];
        CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.SetTile(gridPosition, gateTileBase);
    }
}
