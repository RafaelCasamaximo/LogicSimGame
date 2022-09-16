using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A mesma coisa que o singleton, porém esse não é destruido ao trocar de cena
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingletonPersistent<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        base.Awake();
    }
}
