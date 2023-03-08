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

public class GateRenderer : MonoBehaviour
{
    public LogicGate logicGate;
    public TileBase gateTileBase;
    private Gate gate;
    private Vector2Int size;
    // InputLocations and outputLocation is set according to the
    // Top-left element as (1,1) and the bottom-right as (n, n).
    private List<Vector2Int> inputLocations = new List<Vector2Int>();
    private Vector2Int outputLocation;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void Initialize(Tilemap tilemap, Vector3Int gridPosition)
    {
        switch (logicGate)
        {
            case LogicGate.Generator:
                gate = new GeneratorGate(false);
                size = new Vector2Int(1, 1);
                outputLocation = new Vector2Int(1, 1);
                break;
            case LogicGate.AND:
                gate = new ANDGate();
                size = new Vector2Int(3, 3);
                inputLocations.Add(new Vector2Int(1, 1));
                inputLocations.Add(new Vector2Int(3, 1));
                outputLocation = new Vector2Int(2, 3);
                break;
            case LogicGate.NAND:
                gate = new NANDGate();
                size = new Vector2Int(3, 3);
                inputLocations.Add(new Vector2Int(1, 1));
                inputLocations.Add(new Vector2Int(3, 1));
                outputLocation = new Vector2Int(2, 3);
                break;
            case LogicGate.OR:
                gate = new ORGate();
                size = new Vector2Int(3, 3);
                inputLocations.Add(new Vector2Int(1, 1));
                inputLocations.Add(new Vector2Int(3, 1));
                outputLocation = new Vector2Int(2, 3);
                break;
            case LogicGate.NOR:
                gate = new NORGate();
                size = new Vector2Int(3, 3);
                inputLocations.Add(new Vector2Int(1, 1));
                inputLocations.Add(new Vector2Int(3, 1));
                outputLocation = new Vector2Int(2, 3);
                break;
            case LogicGate.XOR:
                gate = new XORGate();
                size = new Vector2Int(3, 3);
                inputLocations.Add(new Vector2Int(1, 1));
                inputLocations.Add(new Vector2Int(3, 1));
                outputLocation = new Vector2Int(2, 3);
                break;
            case LogicGate.XNOR:
                gate = new XNORGate();
                size = new Vector2Int(3, 3);
                inputLocations.Add(new Vector2Int(1, 1));
                inputLocations.Add(new Vector2Int(3, 1));
                outputLocation = new Vector2Int(2, 3);
                break;
            case LogicGate.NOT:
                gate = new NOTGate();
                size = new Vector2Int(2, 1);
                inputLocations.Add(new Vector2Int(1, 1));
                outputLocation = new Vector2Int(1, 2);
                break;
            case LogicGate.Reader:
                gate = new ReaderGate();
                size = new Vector2Int(1, 1);
                inputLocations.Add(new Vector2Int(1, 1));
                break;
        }

        tilemap.SetTile(gridPosition, gateTileBase);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
