using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script utilizado para iniciar a Cena do CircuitSimulator
/// </summary>
public class CircuitSimulatorInitialize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.ChangeState(GameState.Start);
    }
}
