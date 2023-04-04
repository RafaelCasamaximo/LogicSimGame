using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GENERATORGate : Gate
{

    private SpriteRenderer spriteRenderer;
    public Sprite activeSprite;
    public Sprite disableSprite;
    
    public override void Initialize(Vector3Int gridPosition)
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        
        properties.gridPosition = gridPosition;
        input1 = new Input(new Vector3Int(gridPosition.x, gridPosition.y), null, false, false);
        output = new Output(new Vector3Int(gridPosition.x, gridPosition.y), new List<Tuple<Gate, int>>(), false);

        SetSprite();
        
        transform.position = CircuitSimulatorManager.Instance.circuitSimulatorRenderer.logicGatesTileMap.GetCellCenterWorld(gridPosition);
    }
    
    public override bool Execute()
    {
        return input1.state;
    }

    public void SetState(bool state)
    {
        input1.state = state;
        // output.state = Execute();
        OnInputChanged();
        SetSprite();
    }

    public void SetSprite()
    {
        if (output.state)
        {
            spriteRenderer.sprite = activeSprite;
        }
        else
        {
            spriteRenderer.sprite = disableSprite;
        }
    }
    
}
