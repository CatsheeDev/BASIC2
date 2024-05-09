








#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Pixelplacement
{
    public class EditorUtilities : Editor
    {
        
        
        
        
        
        public static void Error (string errorMessage)
        {
            EditorUtility.DisplayDialog ("Framework Error", errorMessage, "OK");
        }
    }
}
#endif