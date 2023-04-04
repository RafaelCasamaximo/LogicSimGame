using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GENERATORGate : Gate
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
        return input1.state;
    }

    public void SetState(bool state)
    {
        input1.state = state;
        output.state = input1.state;
        OnInputChanged();
    }
    
}
