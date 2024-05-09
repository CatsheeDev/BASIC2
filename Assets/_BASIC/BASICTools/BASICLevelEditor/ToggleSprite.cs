using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
[RequireComponent(typeof(Sprite))]
public class ToggleSprite : MonoBehaviour
{
    [SerializeField] private Sprite _offSprite;
    [SerializeField] private Sprite _onSprite;

    public void onToggleChange(bool enabled)
    {
        if (enabled)
        {
            this.GetComponent<Image>().sprite = _onSprite;
        }
        else
        {
            this.GetComponent<Image>().sprite = _offSprite;
        }
    }
}
