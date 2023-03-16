using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ReaderGate : Gate
{
    public TileBase readerRedTileBase;
    public TileBase readerBlueTileBase;
    public string label;
    
    public override void AddInput(Gate gate)
    {
        inputs.Add(gate);
        gate.outputs.Add(this);
        gate.OutputChanged += OnInputChanged;
        gate.OutputChanged += UpdateState;
        WireRenderer wire = gameObject.AddComponent<WireRenderer>();
        wire.Initialize(gate.outputLocation, inputLocations[inputs.Count - 1], gate.GetOutput());
        gate.outputWires.Add(wire);
        OnInputChanged();
        UpdateState();
    }
    
    public void UpdateState()
    {
        if (output)
        {
            CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.SetTile(position, readerBlueTileBase);
        }

        if (!output)
        {
            CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.SetTile(position, readerRedTileBase);
        }
    }

    protected override bool Execute()
    {
        bool input0 = inputs.ElementAtOrDefault(0) != null && inputs[0].GetOutput();
        return input0;
    }
    
    public override void Initialize(Vector3Int gridPosition)
    {
        outputs = new List<Gate>();
        position = gridPosition;
        outputWires = new List<WireRenderer>();
        size = new Vector2Int(0 + position.x, 0 + position.y);
        inputLocations.Add(new Vector2Int(0 + gridPosition.x, 0 + gridPosition.y));
        gateTileBase = CircuitSimulatorManager.Instance.logicGatesTiles[(int)LogicGate.Reader];
        readerRedTileBase = CircuitSimulatorManager.Instance.logicGatesTiles[(int)LogicGate.ReaderRed];
        readerBlueTileBase = CircuitSimulatorManager.Instance.logicGatesTiles[(int)LogicGate.ReaderBlue];
        CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.SetTile(gridPosition, gateTileBase);
    }

    public void SetLabel(string value)
    {
        label = value;
    }
    
    public string GetLabel()
    {
        return label;
    }
}
