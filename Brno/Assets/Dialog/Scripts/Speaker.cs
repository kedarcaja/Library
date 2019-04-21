using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/Speaker", fileName = "NewSpeaker")]
public class Speaker : ScriptableObject
{
    [SerializeField]
    private Color nameColor;

    private Color NameColor { get => new Color(nameColor.r, nameColor.g, nameColor.b, 1); }

    public override string ToString()
    {
        return string.Format("<color=#" + ColorUtility.ToHtmlStringRGBA(NameColor) + ">" + name + "</color>");
    }

}
