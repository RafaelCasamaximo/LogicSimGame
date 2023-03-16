using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NOTGate : Gate
{
    protected override bool Execute()
    {
        bool input0 = inputs.ElementAtOrDefault(0) != null && inputs[0].GetOutput();
        return !input0;
    }
    
    public override void Initialize(Vector3Int gridPosition)
    {
        outputs = new List<Gate>();
        outputWires = new List<WireRenderer>();
        size = new Vector2Int(1, 1);
        position = gridPosition;
        inputLocations.Add(new Vector2Int(gridPosition.x, gridPosition.y));
        outputLocation = new Vector2Int(gridPosition.x, gridPosition.y);
        gateTileBase = CircuitSimulatorManager.Instance.logicGatesTiles[(int)LogicGate.NOT];
        CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.SetTile(gridPosition, gateTileBase);
    }
}
