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
        var g1 = new GeneratorGate(true);
        var g2 = new GeneratorGate(false);
        var g3 = new GeneratorGate(true);
        
        // Faz AND entre g1 e g2
        var and = new ANDGate();
        and.AddInput(g1);
        and.AddInput(g2);
        
        // Faz OR entre resultado da AND e g3
        var or = new ORGate();
        or.AddInput(and);
        or.AddInput(g3);
        
        // Salva no leitor o resultado da AND
        var r1 = new ReaderGate();
        r1.AddInput(and);
        
        // Salva no leitor o resultado da OR entre o resultado da AND e g3
        var r2 = new ReaderGate();
        r2.AddInput(or);
        
        // Imprime Resultados
        Debug.Log("Saida TRUE [AND] FALSE: " + r1.GetOutput());
        Debug.Log("Saida (TRUE [AND] FALSE) [OR] TRUE: " + r2.GetOutput());
        
        
        g3.SetValue(false);
        Debug.Log("Saida TRUE [AND] FALSE: " + r1.GetOutput());
        Debug.Log("Saida (TRUE [AND] FALSE) [OR] FALSE: " + r2.GetOutput());

    }
}
