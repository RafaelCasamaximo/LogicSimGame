using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANDGate : Gate
{
    
    
    protected override bool Execute()
    {
        return inputs[0].GetOutput() && inputs[1].GetOutput();
    }
    
    public override void Initialize(Vector3Int gridPosition)
    {
        outputWires = new List<WireRenderer>();
        size = new Vector2Int(3, 3);
        position = gridPosition;
        inputLocations.Add(new Vector2Int(gridPosition.x - 1, gridPosition.y + 1));
        inputLocations.Add(new Vector2Int(gridPosition.x - 1, gridPosition.y - 1));
        outputLocation = new Vector2Int(gridPosition.x + 1, gridPosition.y);
        gateTileBase = CircuitSimulatorManager.Instance.logicGatesTiles[(int)LogicGate.AND];
        CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.SetTile(gridPosition, gateTileBase);
    }
}
