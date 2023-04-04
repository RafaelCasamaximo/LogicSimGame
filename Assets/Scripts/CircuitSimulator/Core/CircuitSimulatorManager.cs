using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;


/// <summary>
/// Esse script é responsável pela instanciação dos módulos do CircuitSimulator.
/// Os parâmetros devem ser instanciados aqui e passados para os objetos através do constructor ou de setters
/// </summary>
public class CircuitSimulatorManager : Singleton<CircuitSimulatorManager>
{
    [Header("Manager Settings")]
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

    [Space(10)]
    [Header("Logic Gates Prefabs")]
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

        var g1 = Instantiate(Generator, logicGatesParent.transform);
        g1.GetComponent<GENERATORGate>().Initialize(new Vector3Int(3, 11));
        g1.GetComponent<GENERATORGate>().SetState(true);
        
        var g2 = Instantiate(Generator, logicGatesParent.transform);
        g2.GetComponent<GENERATORGate>().Initialize(new Vector3Int(3, 9));
        g2.GetComponent<GENERATORGate>().SetState(true);
        
        var g3 = Instantiate(Generator, logicGatesParent.transform);
        g3.GetComponent<GENERATORGate>().Initialize(new Vector3Int(3, 7));
        g3.GetComponent<GENERATORGate>().SetState(true);
        
        var and = Instantiate(AND, logicGatesParent.transform);
        and.GetComponent<ANDGate>().Initialize(new Vector3Int(8, 10));
        and.GetComponent<ANDGate>().ChangeInput1(g1);
        and.GetComponent<ANDGate>().ChangeInput2(g2);

        var not1 = Instantiate(NOT, logicGatesParent.transform);
        not1.GetComponent<NOTGate>().Initialize(new Vector3Int(5, 7));
        not1.GetComponent<NOTGate>().ChangeInput1(g3);
        
        var not2 = Instantiate(NOT, logicGatesParent.transform);
        not2.GetComponent<NOTGate>().Initialize(new Vector3Int(7, 8));
        not2.GetComponent<NOTGate>().ChangeInput1(g2);

        var nor = Instantiate(NOR, logicGatesParent.transform);
        nor.GetComponent<NORGate>().Initialize(new Vector3Int(10, 7));
        nor.GetComponent<NORGate>().ChangeInput1(not2);
        nor.GetComponent<NORGate>().ChangeInput2(not1);
        
        var xor = Instantiate(XOR, logicGatesParent.transform);
        xor.GetComponent<XORGate>().Initialize(new Vector3Int(14, 9));
        xor.GetComponent<XORGate>().ChangeInput1(and);
        xor.GetComponent<XORGate>().ChangeInput2(nor);

        GameManager.Instance.ChangeState(GameState.CircuitSimulatorMoving);
        circuitSimulatorPlayerInput.SwitchCurrentActionMap("Movement");
        SoundManager.Instance.PlayMusic(0);
        SoundManager.Instance.ChangeMusicVolume(0.015f);
        
        StartCoroutine(changeGenerators(g1, g2, g3));
        
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

    public IEnumerator changeGenerators(GameObject go1, GameObject go2, GameObject go3)
    {
        yield return new WaitForSeconds(0.7f);
        go2.GetComponent<GENERATORGate>().SetState(!go2.GetComponent<GENERATORGate>().output.state);
        // yield return new WaitForSeconds(0.7f);
        // go2.GetComponent<GENERATORGate>().SetState(!go2.GetComponent<GENERATORGate>().output.state);
        // yield return new WaitForSeconds(0.7f);
        // go3.GetComponent<GENERATORGate>().SetState(!go3.GetComponent<GENERATORGate>().output.state);
        StartCoroutine(changeGenerators(go1, go2, go3));
    }
    
}