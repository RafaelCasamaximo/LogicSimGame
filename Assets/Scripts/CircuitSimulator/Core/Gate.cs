using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Gate : MonoBehaviour
{
    /// <summary>
    /// This variables are responsible for the
    /// gate logic. Not the graphic logic
    /// </summary>
    public List<Gate> inputs = new List<Gate>();

    public List<Gate> outputs = new List<Gate>();
    public event Action OutputChanged;
    protected bool output;

    /// <summary>
    /// This part is Responsible for the
    /// gate graphics.
    /// </summary>
    public Vector3Int position;
    public TileBase gateTileBase;
    public Vector2Int size;
    public List<Vector2Int> inputLocations = new List<Vector2Int>();
    public Vector2Int outputLocation;
    public List<WireRenderer> outputWires = new List<WireRenderer>();


    public virtual void AddInput(Gate gate)
    {
        inputs.Add(gate);
        gate.outputs.Add(this);
        gate.OutputChanged += OnInputChanged;
        WireRenderer wire = gameObject.AddComponent<WireRenderer>();
        wire.Initialize(gate.outputLocation, inputLocations[inputs.Count - 1], gate.GetOutput());
        gate.outputWires.Add(wire);
        OnInputChanged();
    }
    
    public virtual void SetInput(int index, Gate gate)
    {
        if (index >= 0 && index < inputs.Count) {
            // OutputChanged -= inputs[index].OnInputChanged;
            // inputs[index] = gate;
            // OutputChanged += inputs[index].OnInputChanged;

            inputs[index].OutputChanged -= OnInputChanged;
            inputs[index] = gate;
            inputs[index].OutputChanged += OnInputChanged;
            OnInputChanged();
        } else {
            throw new IndexOutOfRangeException("Invalid input index");
        }
    }

    public virtual bool GetOutput()
    {
        return output;
    }

    public virtual void SetOutput(bool value)
    {
        output = value;
        OutputChanged?.Invoke();
        foreach (var wires in outputWires)
        {
            wires.UpdateState(output);
        }
    }

    public virtual void OnInputChanged()
    {
        output = Execute();
        OutputChanged?.Invoke();
        foreach (var wires in outputWires)
        {
            wires.UpdateState(output);
        }
    }

    public virtual void Delete()
    {
        // Definir o tile como null (deletar o tile)
        CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.SetTile(position, null);
        
        // Iterar todos os gates que eu era input e verificar se eu estava conectado na primeira entrada
        // Se eu estava na primeira entrada, então preciso verificar se existe algo na segunda e definir essa entrada como a primeira
        // E também preciso atualizar o fio

        // Iterar por todos os fios que conecta em algo e deletar o line renderer deles
        foreach (var wire in outputWires.ToList())
        {
            wire.RemoveLineRenderer();
            outputWires.Remove(wire);
        }
        // Procurar por fios que terminam no mesmo lugar dos seus inputs
        foreach (var gate in inputs)
        {
            foreach (var wire in gate.outputWires.ToList())
            {
                // Deleta o fio de qualquer input que termina na mesma posição onde fica uma entrada desse gate
                if (wire.CompareEndPoint(inputLocations[0]) || wire.CompareEndPoint(inputLocations[1]))
                {
                    wire.RemoveLineRenderer();
                    gate.outputWires.Remove(wire);
                }
            }
        }
        
        // // Iterar sobre todos os gates que eu era input e atualizar a entrada deles
        // foreach (var output in outputs)
        // {
        //
        // }
        //

        
    }
    
    protected abstract bool Execute();

    public abstract void Initialize(Vector3Int gridPosition);

}
