using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public enum LogicGate
{
    Generator = 0,
    GeneratorRed = 1,
    GeneratorBlue = 2,
    AND = 3,
    NAND = 4,
    OR = 5,
    NOR = 6,
    XOR = 7,
    XNOR = 8,
    NOT = 9,
    Reader = 10,
    ReaderRed = 11,
    ReaderBlue = 12,
}

/// <summary>
/// Esse script é responsável pela instanciação dos módulos do CircuitSimulator.
/// Os parâmetros devem ser instanciados aqui e passados para os objetos através do constructor ou de setters
/// </summary>
public class CircuitSimulatorManager : Singleton<CircuitSimulatorManager>
{
    public Grid grid;
    public int backgroundWidth;
    public int backgroundHeight;
    public List<TileBase> logicGatesTiles;

    /// <summary>
    /// Módulo de gerenciamento do circuitSimulator
    /// </summary>
    public CircuitSimulatorRenderer circuitSimulatorRenderer;
    void Start()
    {
        grid = GetComponent<Grid>();
        circuitSimulatorRenderer = GetComponent<CircuitSimulatorRenderer>();
        circuitSimulatorRenderer.width = backgroundWidth;
        circuitSimulatorRenderer.height = backgroundHeight;
        circuitSimulatorRenderer.FillBackground();
        
        // Gera 3 fontes
        var g1 = gameObject.AddComponent<GeneratorGate>();
        var g2 = gameObject.AddComponent<GeneratorGate>();
        var g3 = gameObject.AddComponent<GeneratorGate>();
        g1.Initialize(new Vector3Int(3, 9));
        g2.Initialize(new Vector3Int(3, 8));
        g3.Initialize(new Vector3Int(3, 7));
        g1.SetValue(true);
        g2.SetValue(false);
        g3.SetValue(true);
        // Faz AND entre g1 e g2
        var and = gameObject.AddComponent<ANDGate>();
        and.Initialize(new Vector3Int(6, 10));
        and.AddInput(g1);
        and.AddInput(g2);
        // Faz OR entre resultado da AND e g3
        var or = gameObject.AddComponent<ORGate>();
        or.Initialize(new Vector3Int(10, 7));
        or.AddInput(and);
        or.AddInput(g3);
        //Cria uma NOT
        var not = gameObject.AddComponent<NOTGate>();
        not.Initialize(new Vector3Int(6, 4));
        not.AddInput(g3);
        //Cria uma XNOR
        var xnor = gameObject.AddComponent<XNORGate>();
        xnor.Initialize(new Vector3Int(10, 4));
        xnor.AddInput(and);
        xnor.AddInput(not);
        // Salva no leitor o resultado da AND
        var r1 = gameObject.AddComponent<ReaderGate>();
        r1.Initialize(new Vector3Int(14, 10));
        r1.AddInput(and);
        // Salva no leitor o resultado da OR entre o resultado da AND e g3
        var r2 = gameObject.AddComponent<ReaderGate>();
        r2.Initialize(new Vector3Int(14, 7));
        r2.AddInput(or);
        // Salva no leitor o resultado entre o NOT R3 e o AND de G1 e G2
        var r3 = gameObject.AddComponent<ReaderGate>();
        r3.Initialize(new Vector3Int(14, 4));
        r3.AddInput(xnor);

        StartCoroutine(ChangeGenerator(g1, g2, g3));
    }
    
    IEnumerator ChangeGenerator(GeneratorGate g1, GeneratorGate g2, GeneratorGate g3)
    {
        yield return new WaitForSeconds(1);
        g1.SetValue(!g1.GetOutput());
        yield return new WaitForSeconds(1);
        g2.SetValue(!g2.GetOutput());
        yield return new WaitForSeconds(1);
        g3.SetValue(!g3.GetOutput());
        StartCoroutine(ChangeGenerator(g1, g2, g3));

    }
}
