using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cover : MonoBehaviour
{
    [SerializeField] private float delay; 
    //too lazy for coroutine
    void Update()
    {
        if (delay <= 0)
        {
            PersistentCamera.Instance?.Transition(TransitionType.Dither, 5, true); 
            GetComponent<Image>().enabled = false;
            Debug.Log("disable");
            Destroy(gameObject);
            Debug.Log("detroy");
            return; 
        }

        delay -= 0.1f; 
    }
}
