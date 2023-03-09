using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NORGateRenderer : GateRenderer
{
    public override void Initialize(Vector3Int gridPosition)
    {
        gate = new NORGate();
        size = new Vector2Int(3, 3);
        inputLocations.Add(new Vector2Int(1, 1));
        inputLocations.Add(new Vector2Int(3, 1));
        outputLocation = new Vector2Int(2, 3);
        logicGate = LogicGate.NOR;
        gateTileBase = CircuitSimulatorManager.Instance.logicGatesTiles[(int)LogicGate.NOR];
        CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.SetTile(gridPosition, gateTileBase);
    }
}
