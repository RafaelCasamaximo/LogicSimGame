using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneratorGate : Gate
{
    
    public bool state;
    public TileBase generatorRedTileBase;
    public TileBase generatorBlueTileBase;
    

    public GeneratorGate(bool value)
    {
        state = value;
        SetOutput(state);
    }
    
    public void SetValue(bool value)
    {
        state = value;
        SetOutput(state);
        UpdateState();
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
        outputs = new List<Gate>();
        position = gridPosition;
        outputWires = new List<WireRenderer>();
        size = new Vector2Int(0 + position.x, 0 + position.y);
        outputLocation = new Vector2Int(position.x, position.y);
        gateTileBase = CircuitSimulatorManager.Instance.logicGatesTiles[(int)LogicGate.Generator];
        generatorRedTileBase = CircuitSimulatorManager.Instance.logicGatesTiles[(int)LogicGate.GeneratorRed];
        generatorBlueTileBase = CircuitSimulatorManager.Instance.logicGatesTiles[(int)LogicGate.GeneratorBlue];
        CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.SetTile(gridPosition, gateTileBase);
    }

    public void UpdateState()
    {
        if (state)
        {
            CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.SetTile(position, generatorBlueTileBase);
        }

        if (!state)
        {
            CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.SetTile(position, generatorRedTileBase);
        }
    }
}
