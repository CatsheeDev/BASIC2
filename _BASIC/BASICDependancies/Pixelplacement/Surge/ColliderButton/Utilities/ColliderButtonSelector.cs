








using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class ColliderButtonSelector : MonoBehaviour
{
    
    public Chooser chooser;
    public bool loopAround;
    public KeyCode previousKey = KeyCode.LeftArrow;
    public KeyCode nextKey = KeyCode.RightArrow;
    public ColliderButton[] colliderButtons;

    
    private int _index;

    
    private void OnEnable()
    {
        _index = -1;
        Next();
    }

    private void Reset()
    {
        chooser = GetComponent<Chooser>();
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(previousKey)) Previous();
        if (Input.GetKeyDown(nextKey)) Next();
    }

    
    public void Next()
    {
        _index++;

        if (_index > colliderButtons.Length-1)
        {
            if (loopAround)
            {
                _index = 0;
            }
            else
            {
                _index = colliderButtons.Length - 1;
            }
        }
    
        chooser.transform.LookAt(colliderButtons[_index].transform);
    }

    public void Previous()
    {
        _index--;

        if (_index < 0)
        {
            if (loopAround)
            {
                _index = colliderButtons.Length - 1;
            }
            else
            {
                _index = 0;
            }
        }

        chooser.transform.LookAt(colliderButtons[_index].transform);
    }
}