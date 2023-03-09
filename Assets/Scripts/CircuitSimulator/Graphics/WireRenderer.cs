using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireRenderer
{
    public LineRenderer wire;
    public Vector3Int startPoint;
    public Vector3Int endPoint;

    public bool state;
    // Start is called before the first frame update

    public WireRenderer(Vector3Int start, Vector3Int end, GameObject gameObject)
    {
        wire = gameObject.AddComponent<LineRenderer>();
        wire.startWidth = 1f;
        wire.endWidth = 1f;
        wire.useWorldSpace = true;
        wire.positionCount = 2;
        wire.SetPosition(0, start);
        wire.SetPosition(1, end);
    }
}
