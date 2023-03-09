using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorGateRenderer : GateRenderer
{
    public override void Initialize(Vector3Int gridPosition)
    {
        gate = new GeneratorGate(false);
        size = new Vector2Int(1, 1);
        outputLocation = new Vector2Int(1, 1);
        logicGate = LogicGate.Generator;
        gateTileBase = CircuitSimulatorManager.Instance.logicGatesTiles[(int)LogicGate.Generator];
        CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.SetTile(gridPosition, gateTileBase);
    }

    public void SetValue(bool value)
    {
        ((GeneratorGate)gate).SetValue(value);
    }
}
