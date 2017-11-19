using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
class PlayerAttributeUI
{
    public ItemAttribute m_Attribute = null;
    public ShowType showType = ShowType.None;
    public Text showText = null;
    public Slider showSlider = null;
}
