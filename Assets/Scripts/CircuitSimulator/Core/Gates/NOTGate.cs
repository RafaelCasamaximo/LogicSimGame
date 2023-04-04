using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOTGate : Gate
{
    
    public virtual void Initialize(Vector3Int gridPosition)
    {
        properties.gridPosition = gridPosition;
        input1 = new Input(new Vector3Int(gridPosition.x, gridPosition.y), null, false, false);
        output = new Output(new Vector3Int(gridPosition.x, gridPosition.y), new List<Tuple<Gate, int>>(), false);

        transform.position = CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.GetCellCenterWorld(gridPosition);

    }
    
    public override bool Execute()
    {
        bool value1 = input1.hasGate && input1.state;
        return !value1;
    }
}
