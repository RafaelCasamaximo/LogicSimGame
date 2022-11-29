using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Interface que apresenta os textos do sistema de dialogos construido. Essa interface é atrelada ao canvas respectivo.
/// Speaker represente o autor do dialogo
/// Sentence representa o texto mostrado na tela
/// </summary>
public class DialogueGUI : GUIControl
{
    public override string key { get { return "DialogueGUI"; } }

    public TMP_Text speaker;

    public TMP_Text sentence;

    public void SetSentence(string newSentence)
    {
        sentence.text = newSentence;
    }

    public void SetSpeaker(string newSpeaker)
    {
        speaker.text = newSpeaker;
    }
}
