using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CircuitSimulatorManager : Singleton<CircuitSimulatorManager>
{
    public int backgroundWidth;
    public int backgroundHeight;
    
    public CircuitSimulatorRenderer circuitSimulatorRenderer;
    void Start()
    {
        circuitSimulatorRenderer = GetComponent<CircuitSimulatorRenderer>();
        circuitSimulatorRenderer.width = backgroundWidth;
        circuitSimulatorRenderer.height = backgroundHeight;
        circuitSimulatorRenderer.FillBackground();
    }
}
