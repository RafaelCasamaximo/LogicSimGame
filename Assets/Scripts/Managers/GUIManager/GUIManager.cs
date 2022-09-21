using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Essa classe é um singleton responsável por gerenciar as inumeras interfaces do jogo
/// Cada vez que um novo Panel for adicionado, ele será registrado no dicionario _GUIs
/// no Awake do gameObject. Do mesmo jeito, ele será removido no OnDestroy do gameObject.
/// Essa classe controla quais interfaces estão abertas simultaneamente e pode controlar
/// quais interfaces fechar também.
///
/// GUIControl é uma outra classe responsável pela implementação dos métodos utilizados aqui
/// Dessa forma, uma nova interface sempre vai herdar a classe GUIControl
///
/// Um exemplo de uso pode ser visto aqui:
/// https://forum.unity.com/threads/managing-state-of-ui.416286/
/// </summary>
public class GUIManager : SingletonPersistent<GUIManager>
{
    private Dictionary<string, GUIControl> _GUIs = new Dictionary<string, GUIControl>();

    public void Register(GUIControl newGUIControl)
    {
        if (newGUIControl is null || _GUIs.ContainsKey(newGUIControl.key)) return;
        _GUIs.Add(newGUIControl.key, newGUIControl);
    }
    

    public void Unregister(GUIControl guiControl)
    {
        if (guiControl is null) return;

        _GUIs.Remove(guiControl.key);
    }

    public void Show(string GUIControlKey)
    {
        GUIControl result = null;
        if (_GUIs.TryGetValue(GUIControlKey, out result) && !result.gameObject.activeSelf)
        {
            result.OnShow();
        }
    }
    
    public void Hide(string GUIControlKey)
    {
        GUIControl result = null;
        if (_GUIs.TryGetValue(GUIControlKey, out result) && result.gameObject.activeSelf)
        {
            result.OnHide();
        }
    }
    
    public void ShowAndHide(string GUIControlKey, GUIControl toHideGUIControl)
    {
        if (GUIControlKey == toHideGUIControl.key) return;

        GUIControl result = null;
        if (_GUIs.TryGetValue(GUIControlKey, out result))
        {
            if (!result.gameObject.activeSelf) result.OnShow();
            if (toHideGUIControl.gameObject.activeSelf) toHideGUIControl.OnHide();
        }
    }

    public void HideAll()
    {
        foreach(string item in _GUIs.Keys)
        {
            Hide(item);
        }
    }

}
