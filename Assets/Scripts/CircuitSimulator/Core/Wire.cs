using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public LineRenderer wire;
    public Vector3 startPoint;
    public Vector3 endPoint;

    public void Initialize(GameObject gateGO, Vector3Int start, Vector3Int end, bool state)
    {
        // Define os pontos
        startPoint = CircuitSimulatorManager.Instance.grid.GetCellCenterWorld(start);
        startPoint.z = -0.01f;
        endPoint = CircuitSimulatorManager.Instance.grid.GetCellCenterWorld(end);
        endPoint.z = -0.01f;
        
        // Cria objeto e define como um filho do objeto que tem o wire como output
        GameObject wireObject = new GameObject("Wire");
        wireObject.transform.SetParent(gateGO.transform);
        
        // Adiciona o Line Renderer e define as propriedades
        wire = wireObject.AddComponent<LineRenderer>();
        wire.startWidth = 0.1f;
        wire.endWidth = 0.1f;
        wire.numCornerVertices = 5;
        wire.numCapVertices = 5;
        wire.sortingOrder = 1;
        wire.useWorldSpace = true;
        wire.positionCount = 4;
        wire.material = new Material(Shader.Find("Sprites/Default"));
        
        // Set the color as blue or red depending on the state
        UpdateColor(state);
        
        // Add middle points to break the line into a 3-piece line
        wire.SetPosition(0, startPoint);
        wire.SetPosition(1, new Vector3(startPoint.x + 0.7f, startPoint.y, startPoint.z));
        wire.SetPosition(2, new Vector3(endPoint.x + -0.7f, endPoint.y, endPoint.z));
        wire.SetPosition(3, endPoint);
    }

    public void UpdateColor(bool state)
    {
        // Deixa azul ou vermelho dependendo do estado
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
