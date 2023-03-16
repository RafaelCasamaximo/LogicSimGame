using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NORGate : Gate
{
    protected override bool Execute()
    {
        bool input0 = inputs.ElementAtOrDefault(0) != null && inputs[0].GetOutput();
        bool input1 = inputs.ElementAtOrDefault(1) != null && inputs[1].GetOutput();
        return !(input0 || input1);
    }
    
    public override void Initialize(Vector3Int gridPosition)
    {
        outputs = new List<Gate>();
        outputWires = new List<WireRenderer>();
        size = new Vector2Int(3, 3);
        position = gridPosition;
        inputLocations.Add(new Vector2Int(gridPosition.x - 1, gridPosition.y + 1));
        inputLocations.Add(new Vector2Int(gridPosition.x - 1, gridPosition.y - 1));
        outputLocation = new Vector2Int(gridPosition.x + 1, gridPosition.y);
        gateTileBase = CircuitSimulatorManager.Instance.logicGatesTiles[(int)LogicGate.NOR];
        CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.SetTile(gridPosition, gateTileBase);
    }
}
