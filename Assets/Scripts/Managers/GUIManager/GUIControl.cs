using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Essa classe é responsável pela comunicação entre o gameObject da interface e
/// o GUIManager. Toda interface deve herdar essa classe e sobrescrever a key.
///
/// Um exemplo de uso pode ser visto aqui:
/// https://forum.unity.com/threads/managing-state-of-ui.416286/
/// </summary>
[DefaultExecutionOrder (-20)]
public abstract class GUIControl : MonoBehaviour
{
    public abstract string key { get; }

    protected virtual void Start()
    {
        GUIManager.Instance.Register(this);
        GUIManager.Instance.Hide(key);
    }

    // private void OnDestroy()
    // {
    //     Debug.Log(GUIManager.Instance);
    //     GUIManager.Instance.Unregister(this);
    // }

    public virtual void OnShow()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnHide()
    {
        gameObject.SetActive(false);
    }
}
