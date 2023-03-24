using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
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
    public PlayerInput circuitSimulatorPlayerInput;
    public Grid grid;
    public int backgroundWidth;
    public int backgroundHeight;
    public List<TileBase> logicGatesTiles;

    // Armazena os Gates que existem na cena para deleção e outras coisas.
    public List<Gate> gates = new List<Gate>();

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
        gates.Add(g1);
        var g2 = gameObject.AddComponent<GeneratorGate>();
        gates.Add(g2);
        var g3 = gameObject.AddComponent<GeneratorGate>();
        gates.Add(g3);
        g1.Initialize(new Vector3Int(3, 9));
        g2.Initialize(new Vector3Int(3, 8));
        g3.Initialize(new Vector3Int(3, 7));
        g1.SetValue(true);
        g2.SetValue(false);
        g3.SetValue(true);
        // Faz AND entre g1 e g2
        var and = gameObject.AddComponent<ANDGate>();
        gates.Add(and);
        and.Initialize(new Vector3Int(6, 10));
        and.AddInput(g1);
        and.AddInput(g2);
        // Faz OR entre resultado da AND e g3
        var or = gameObject.AddComponent<ORGate>();
        gates.Add(or);
        or.Initialize(new Vector3Int(10, 7));
        or.AddInput(and);
        or.AddInput(g3);
        //Cria uma NOT
        var not = gameObject.AddComponent<NOTGate>();
        gates.Add(not);
        not.Initialize(new Vector3Int(6, 4));
        not.AddInput(g3);
        //Cria uma XNOR
        var xnor = gameObject.AddComponent<XNORGate>();
        gates.Add(xnor);
        xnor.Initialize(new Vector3Int(10, 4));
        xnor.AddInput(and);
        xnor.AddInput(not);
        // Salva no leitor o resultado da AND
        var r1 = gameObject.AddComponent<ReaderGate>();
        gates.Add(r1);
        r1.Initialize(new Vector3Int(14, 10));
        r1.AddInput(and);
        // Salva no leitor o resultado da OR entre o resultado da AND e g3
        var r2 = gameObject.AddComponent<ReaderGate>();
        gates.Add(r2);
        r2.Initialize(new Vector3Int(14, 7));
        r2.AddInput(or);
        // Salva no leitor o resultado entre o NOT R3 e o AND de G1 e G2
        var r3 = gameObject.AddComponent<ReaderGate>();
        gates.Add(r3);
        r3.Initialize(new Vector3Int(14, 4));
        r3.AddInput(xnor);

        StartCoroutine(ChangeGenerator(g1, g2, g3));
        
        
        circuitSimulatorPlayerInput.SwitchCurrentActionMap("Movement");
    }

    // Itera sobre a lista de Gates que foram inseridas pelo o jogador e deleta todos
    // Deleta os fios também. É um soft reset da cena.
    public void DeleteAllGates()
    {
        foreach (var gate in gates.ToList())
        {
            foreach (var wire in gate.outputWires.ToList())
            {
                wire.RemoveLineRenderer();
                Destroy(wire);
            }
            Destroy(gate);
        }
        circuitSimulatorRenderer.logicGatesTileMap.ClearAllTiles();
    }
    
    // Função de teste e vai ser deletada depois
    IEnumerator ChangeGenerator(GeneratorGate g1, GeneratorGate g2, GeneratorGate g3)
    {
        yield return new WaitForSeconds(0.2f);
        g1.SetValue(!g1.GetOutput());
        yield return new WaitForSeconds(0.2f);
        g2.SetValue(!g2.GetOutput());
        yield return new WaitForSeconds(0.2f);
        g3.SetValue(!g3.GetOutput());
        StartCoroutine(ChangeGenerator(g1, g2, g3));

    }
}
