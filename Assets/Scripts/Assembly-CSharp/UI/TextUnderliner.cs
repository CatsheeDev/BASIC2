using System;
using TMPro;
using UnityEngine;


public class TextUnderliner : MonoBehaviour
{
    
    public void Underline()
    {
        this.text.fontStyle = FontStyles.Underline;
    }

    
    public void Ununderline()
    {
        this.text.fontStyle = FontStyles.Normal;
    }

    
    public TMP_Text text;
}
