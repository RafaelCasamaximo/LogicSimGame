using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NANDGate : Gate
{
    protected override bool Execute()
    {
        return !(inputs[0].GetOutput() && inputs[1].GetOutput());
    }
    
    public override void Initialize(Vector3Int gridPosition)
    {
        outputWires = new List<WireRenderer>();
        size = new Vector2Int(3, 3);
        inputLocations.Add(new Vector2Int(1, 1));
        inputLocations.Add(new Vector2Int(3, 1));
        outputLocation = new Vector2Int(2, 3);
        gateTileBase = CircuitSimulatorManager.Instance.logicGatesTiles[(int)LogicGate.NAND];
        CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.SetTile(gridPosition, gateTileBase);
    }
}
