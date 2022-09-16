using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
