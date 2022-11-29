using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Essa classe é atrelada ao canvas de carregamento de tela.
/// NOTE: Futuramente é possível adicionar uma barra de loading ou animação aqui caso necessário
/// </summary>
public class LoadingGUI : GUIControl
{
    public override string key { get { return "LoadingGUI"; } }
}
