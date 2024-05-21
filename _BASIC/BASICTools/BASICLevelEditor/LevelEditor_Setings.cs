using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelEditor_Setings : MonoBehaviour
{
    [System.Serializable]
    public class objectFolder
    {
        public Sprite preview;
        public ObjectProfile2 objects;

        [HideInInspector]
        public bool openFolder;
    }


    public objectFolder[] folders;
}
