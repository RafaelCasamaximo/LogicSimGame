using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Esse script é responsável pela instanciação dos módulos do CircuitSimulator.
/// Os parâmetros devem ser instanciados aqui e passados para os objetos através do constructor ou de setters
/// </summary>
public class CircuitSimulatorManager : Singleton<CircuitSimulatorManager>
{
    public int backgroundWidth;
    public int backgroundHeight;
    public List<TileBase> logicGatesTiles;

    /// <summary>
    /// Módulo de renderização do circuitSimulator
    /// </summary>
    public CircuitSimulatorRenderer circuitSimulatorRenderer;
    void Start()
    {
        circuitSimulatorRenderer = GetComponent<CircuitSimulatorRenderer>();
        circuitSimulatorRenderer.width = backgroundWidth;
        circuitSimulatorRenderer.height = backgroundHeight;
        circuitSimulatorRenderer.FillBackground();
        
        // Gera 3 fontes
        var g1 = gameObject.AddComponent<GeneratorGateRenderer>();
        g1.Initialize(new Vector3Int(3, 9));
        var g2 = gameObject.AddComponent<GeneratorGateRenderer>();
        g2.Initialize(new Vector3Int(3, 8));
        var g3 = gameObject.AddComponent<GeneratorGateRenderer>();
        g3.Initialize(new Vector3Int(3, 7));
        g1.SetValue(true);
        g2.SetValue(false);
        g3.SetValue(true);
        // Faz AND entre g1 e g2
        var and = gameObject.AddComponent<ANDGateRenderer>();
        and.Initialize(new Vector3Int(6, 10));
        and.AddInput(g1);
        and.AddInput(g2);
        // Faz OR entre resultado da AND e g3
        var or = gameObject.AddComponent<ORGateRenderer>();
        or.Initialize(new Vector3Int(10, 7));
        or.AddInput(and);
        or.AddInput(g3);
        // Salva no leitor o resultado da AND
        var r1 = gameObject.AddComponent<ReaderGateRenderer>();
        r1.Initialize(new Vector3Int(14, 10));
        r1.AddInput(and);
        // Salva no leitor o resultado da OR entre o resultado da AND e g3
        var r2 = gameObject.AddComponent<ReaderGateRenderer>();
        r2.Initialize(new Vector3Int(14, 7));
        r2.AddInput(or);
        // Imprime Resultados
        Debug.Log("Saida TRUE [AND] FALSE: " + r1.GetOutput());
        Debug.Log("Saida (TRUE [AND] FALSE) [OR] TRUE: " + r2.GetOutput());
        // Altera o valor e faz a propagation
        g3.SetValue(false);
        Debug.Log("Saida TRUE [AND] FALSE: " + r1.GetOutput());
        Debug.Log("Saida (TRUE [AND] FALSE) [OR] FALSE: " + r2.GetOutput());

    }
}
