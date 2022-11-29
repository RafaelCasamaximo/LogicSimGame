using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script utilizado para inicializar a cena do TestPlayeground
/// </summary>
public class TestPlaygroundInitialize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.ChangeState(GameState.Start);
    }
}
