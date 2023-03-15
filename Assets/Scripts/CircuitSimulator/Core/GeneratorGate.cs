using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeneratorGate : Gate
{
    
    public bool state;

    public GeneratorGate(bool value)
    {
        state = value;
        SetOutput(state);
    }
    
    public void SetValue(bool value)
    {
        state = value;
        SetOutput(state);
    }
    
    public override void AddInput(Gate gate)
    {
        // Warning.Caution("The GeneratorGate shouldn't have an AddInput!");
    }

    
    protected override bool Execute()
    {
        return state;
    }
    
    public override void Initialize(Vector3Int gridPosition)
    {
        position = gridPosition;
        outputWires = new List<WireRenderer>();
        size = new Vector2Int(0 + position.x, 0 + position.y);
        outputLocation = new Vector2Int(position.x, position.y);
        gateTileBase = CircuitSimulatorManager.Instance.logicGatesTiles[(int)LogicGate.Generator];
        CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.SetTile(gridPosition, gateTileBase);
    }
}
