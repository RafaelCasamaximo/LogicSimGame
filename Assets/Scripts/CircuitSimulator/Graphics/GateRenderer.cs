using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public enum LogicGate
{
    Generator = 0,
    AND = 1,
    NAND = 2,
    OR = 3,
    NOR = 4,
    XOR = 5,
    XNOR = 6,
    NOT = 7,
    Reader = 8
}

public abstract class GateRenderer : MonoBehaviour
{
    public LogicGate logicGate { get; set; }
    public TileBase gateTileBase;
    public Gate gate;
    public Vector2Int size;
    // InputLocations and outputLocation is set according to the
    // Top-left element as (1,1) and the bottom-right as (n, n).
    public List<Vector2Int> inputLocations = new List<Vector2Int>();
    public Vector2Int outputLocation;

    public abstract void Initialize(Vector3Int gridPosition);

    public virtual void AddInput(GateRenderer gateRenderer)
    {
        gate.AddInput(gateRenderer.gate);
    }
    
    public virtual void SetInput(int index, GateRenderer gateRenderer)
    {
        gate.SetInput(index, gateRenderer.gate);
    }
    
    public virtual void SetOutput(bool value)
    {
        gate.SetOutput(value);
    }

    public virtual bool GetOutput()
    {
        return gate.GetOutput();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
