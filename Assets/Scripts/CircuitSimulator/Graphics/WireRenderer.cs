using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireRenderer : MonoBehaviour
{
    public LineRenderer wire;
    public Vector3 startPoint;
    public Vector3 endPoint;

    public bool state;

    // Start is called before the first frame update
    public void Initialize(Vector2Int start, Vector2Int end)
    {
        startPoint = CircuitSimulatorManager.Instance.grid.GetCellCenterWorld(new Vector3Int(start.x, start.y));
        startPoint.z = -0.01f;
        endPoint = CircuitSimulatorManager.Instance.grid.GetCellCenterWorld(new Vector3Int(end.x, end.y));
        endPoint.z = -0.01f;
        GameObject wireObject = new GameObject("Wire");
        wireObject.transform.SetParent(transform);
        wire = wireObject.AddComponent<LineRenderer>();
        wire.startWidth = 0.1f;
        wire.endWidth = 0.1f;
        wire.numCornerVertices = 5;
        wire.numCapVertices = 5;
        wire.sortingOrder = 1;
        wire.useWorldSpace = true;
        wire.positionCount = 2;
        wire.material = new Material(Shader.Find("Sprites/Default"));
        wire.startColor = ColorPalette.activatedWire;
        wire.endColor = ColorPalette.activatedWire;
        wire.SetPosition(0, startPoint);
        wire.SetPosition(1, endPoint);
    }
}