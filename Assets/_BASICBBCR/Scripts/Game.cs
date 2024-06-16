using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadGame()
    {
        DontDestroyOnLoad(Instantiate(Resources.Load("PersistentController")) as GameObject);
    }
}
