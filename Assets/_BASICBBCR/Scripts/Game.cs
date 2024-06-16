using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadGame()
    {
        Debug.Log("instnatoie"); 
        DontDestroyOnLoad(Instantiate(Resources.Load("PersistentController")) as GameObject);
    }
}
