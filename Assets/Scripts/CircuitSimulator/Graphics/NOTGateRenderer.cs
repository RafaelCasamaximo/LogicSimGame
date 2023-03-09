using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOTGateRenderer : GateRenderer
{
    public override void Initialize(Vector3Int gridPosition)
    {
        gate = new NOTGate();
        size = new Vector2Int(1, 2);
        inputLocations.Add(new Vector2Int(1, 1));
        outputLocation = new Vector2Int(1, 2);
        logicGate = LogicGate.NOT;
        gateTileBase = CircuitSimulatorManager.Instance.logicGatesTiles[(int)LogicGate.NOT];
        CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.SetTile(gridPosition, gateTileBase);
    }
}
