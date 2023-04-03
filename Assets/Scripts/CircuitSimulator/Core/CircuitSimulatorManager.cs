using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Palmmedia.ReportGenerator.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

// [Serializable]
// public enum LogicGate
// {
//     Generator = 0,
//     AND = 1,
//     NAND = 2,
//     OR = 3,
//     NOR = 4,
//     XOR = 5,
//     XNOR = 6,
//     NOT = 7,
//     Reader = 8,
// }

/// <summary>
/// Esse script é responsável pela instanciação dos módulos do CircuitSimulator.
/// Os parâmetros devem ser instanciados aqui e passados para os objetos através do constructor ou de setters
/// </summary>
public class CircuitSimulatorManager : Singleton<CircuitSimulatorManager>
{
    public PlayerInput circuitSimulatorPlayerInput;
    [HideInInspector] public Grid grid;
    public int backgroundWidth;
    public int backgroundHeight;
    public GameObject logicGatesParent;

    // Armazena os Gates que existem na cena para deleção e outras coisas.
    [HideInInspector] public List<Gate> gates = new List<Gate>();

    /// <summary>
    /// Módulo de gerenciamento do circuitSimulator
    /// </summary>
    [HideInInspector] public CircuitSimulatorRenderer circuitSimulatorRenderer;


    public GameObject Generator;
    public GameObject AND;
    public GameObject NAND;
    public GameObject OR;
    public GameObject NOR;
    public GameObject XOR;
    public GameObject XNOR;
    public GameObject NOT;
    public GameObject Reader;
    
    void Start()
    {
        
        grid = GetComponent<Grid>();
        circuitSimulatorRenderer = GetComponent<CircuitSimulatorRenderer>();
        circuitSimulatorRenderer.width = backgroundWidth;
        circuitSimulatorRenderer.height = backgroundHeight;
        circuitSimulatorRenderer.FillBackground();
        
        // var and = gameObject.AddComponent<ANDGate>();
        // and.Initialize(new Vector3Int(5, 7));
        
        var g1 = Instantiate(Generator, logicGatesParent.transform);
        g1.GetComponent<GENERATORGate>().Initialize(new Vector3Int(3, 9));
        
        var and = Instantiate(AND, logicGatesParent.transform);
        and.GetComponent<ANDGate>().Initialize(new Vector3Int(6, 10));
        
        GameManager.Instance.ChangeState(GameState.CircuitSimulatorMoving);
        circuitSimulatorPlayerInput.SwitchCurrentActionMap("Movement");
        SoundManager.Instance.PlayMusic(0);
        SoundManager.Instance.ChangeMusicVolume(0.015f);
        
    }

    // // Itera sobre a lista de Gates que foram inseridas pelo o jogador e deleta todos
    // // Deleta os fios também. É um soft reset da cena.
    // public void DeleteAllGates()
    // {
    //     foreach (var gate in gates.ToList())
    //     {
    //         foreach (var wire in gate.outputWires.ToList())
    //         {
    //             wire.RemoveLineRenderer();
    //             Destroy(wire);
    //         }
    //         Destroy(gate);
    //     }
    //     circuitSimulatorRenderer.logicGatesTileMap.ClearAllTiles();
    // }
    
}