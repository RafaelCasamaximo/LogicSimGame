using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script responsável pelos dialogos
/// É instanciado e definido no DialogueSystem e invocado no DialogueTrigger e DialogueGUI
/// </summary>
public class Dialogue
{

    public string name;
    [TextArea(3, 10)]
    public string[] sentences;

}