using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireRenderer : MonoBehaviour
{
    public LineRenderer wire;
    public Vector3Int startPoint;
    public Vector3Int endPoint;

    public bool state;

    // Start is called before the first frame update
    public void Initialize(Vector2Int start, Vector2Int end)
    {
        GameObject wireObject = new GameObject("Wire");
        wireObject.transform.SetParent(transform);
        wire = wireObject.AddComponent<LineRenderer>();
        wire.startWidth = 2f;
        wire.endWidth = 2f;
        wire.useWorldSpace = true;
        wire.positionCount = 2;
        wire.SetPosition(0, new Vector3(start.x, start.y));
        wire.SetPosition(1, new Vector3(end.x, end.y));
    }
}