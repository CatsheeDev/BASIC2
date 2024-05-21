








using UnityEngine;
using UnityEditor;
using System;

namespace Pixelplacement
{
    [InitializeOnLoad]
    public class InitializationRequirements
    {
        static InitializationRequirements ()
        {
            
            StateMachine[] stateMachines = Resources.FindObjectsOfTypeAll<StateMachine> ();
            foreach (StateMachine item in stateMachines) 
            {
                if (item.GetComponent<Initialization> () == null) item.gameObject.AddComponent<Initialization> ();	
            }

            
            DisplayObject[] displayObjects = Resources.FindObjectsOfTypeAll<DisplayObject> ();
            foreach (DisplayObject item in displayObjects) 
            {
                if (item.GetComponent<Initialization> () == null) item.gameObject.AddComponent<Initialization> ();	
            }

            
            foreach (GameObject item in Resources.FindObjectsOfTypeAll<GameObject> ()) 
            {
                foreach (Component subItem in item.GetComponents<Component> ())
                {
                    
                    if (subItem == null) continue;

                    string baseType;

                    #if NETFX_CORE
                    baseType = subItem.GetType ().GetTypeInfo ().BaseType.ToString ();
                    #else
                    baseType = subItem.GetType ().BaseType.ToString ();
                    #endif

                    if (baseType.Contains ("Singleton") && baseType.Contains ("Pixelplacement")) 
                    {
                        if (item.GetComponent<Initialization> () == null) item.gameObject.AddComponent<Initialization> ();
                        continue;
                    }
                }
            }
        }
    }
}