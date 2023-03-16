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
    public void Initialize(Vector2Int start, Vector2Int end, bool newState)
    {
        startPoint = CircuitSimulatorManager.Instance.grid.GetCellCenterWorld(new Vector3Int(start.x, start.y));
        startPoint.z = -0.01f;
        endPoint = CircuitSimulatorManager.Instance.grid.GetCellCenterWorld(new Vector3Int(end.x, end.y));
        endPoint.z = -0.01f;
        state = newState;
        GameObject wireObject = new GameObject("Wire");
        wireObject.transform.SetParent(transform);
        wire = wireObject.AddComponent<LineRenderer>();
        wire.startWidth = 0.1f;
        wire.endWidth = 0.1f;
        wire.numCornerVertices = 5;
        wire.numCapVertices = 5;
        wire.sortingOrder = 1;
        wire.useWorldSpace = true;
        wire.positionCount = 4;
        wire.material = new Material(Shader.Find("Sprites/Default"));
        if (state)
        {
            wire.startColor = ColorPalette.activatedWire;
            wire.endColor = ColorPalette.activatedWire;
        }
        if (!state)
        {
            wire.startColor = ColorPalette.deactivatedWire;
            wire.endColor = ColorPalette.deactivatedWire;
        }
        wire.SetPosition(0, startPoint);
        wire.SetPosition(1, new Vector3(startPoint.x + 0.7f, startPoint.y, startPoint.z));
        wire.SetPosition(2, new Vector3(endPoint.x + -0.7f, endPoint.y, endPoint.z));
        wire.SetPosition(3, endPoint);
    }

    public void UpdateState(bool newState)
    {
        state = newState;
        if (state)
        {
            wire.startColor = ColorPalette.activatedWire;
            wire.endColor = ColorPalette.activatedWire;
        }
        if (!state)
        {
            wire.startColor = ColorPalette.deactivatedWire;
            wire.endColor = ColorPalette.deactivatedWire;
        }
    }
}