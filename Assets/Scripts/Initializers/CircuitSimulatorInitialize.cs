using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitSimulatorInitialize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.ChangeState(GameState.Start);
    }
}
