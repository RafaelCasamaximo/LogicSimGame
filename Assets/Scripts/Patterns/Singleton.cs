using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Essa classe é um padrão de projeto que permite que haja apenas uma instancia dela na cena
/// O acesso é feito com [Class].Instance.[methods]
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    protected virtual void Awake() => Instance = this as T;

    protected void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}