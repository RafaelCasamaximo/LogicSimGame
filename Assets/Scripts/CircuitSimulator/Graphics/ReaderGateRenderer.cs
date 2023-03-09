using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaderGateRenderer : GateRenderer
{
    public override void Initialize(Vector3Int gridPosition)
    {
        gate = new ReaderGate();
        size = new Vector2Int(1, 1);
        inputLocations.Add(new Vector2Int(1, 1));
        logicGate = LogicGate.Reader;
        gateTileBase = CircuitSimulatorManager.Instance.logicGatesTiles[(int)LogicGate.Reader];
        CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.SetTile(gridPosition, gateTileBase);
    }

    public void SetLabel(string label)
    {
        ((ReaderGate)gate).label = label;
    }
    
    public string GetLabel()
    {
        return ((ReaderGate)gate).label;
    }
    
}
