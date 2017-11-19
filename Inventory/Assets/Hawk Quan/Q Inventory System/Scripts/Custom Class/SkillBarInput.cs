using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SkillBarInput{
    public int SlotID;
    public KeyCode key;
    public string keyName;
    [HideInInspector]
    public Text keyText;
    [HideInInspector]
    public ItemData data;
}
