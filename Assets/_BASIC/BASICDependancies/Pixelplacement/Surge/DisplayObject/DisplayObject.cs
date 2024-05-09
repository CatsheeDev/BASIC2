








using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Pixelplacement
{
    [RequireComponent (typeof (Initialization))]
    public class DisplayObject : MonoBehaviour 
    {
        
        private bool _activated;

        
        
        
        
        public bool ActiveSelf
        {
            get
            {
                return gameObject.activeSelf;
            }

            set
            {
                SetActive(value);
            }
        }

        
        
        
        
        public void Register ()
        {
            if (!_activated) 
            {
                _activated = true;	
                gameObject.SetActive (false);
            }
        }
        
        
        
        
        public void SetActive (bool value)
        {
            _activated = true;	
            gameObject.SetActive (value);
        }

        
        
        
        public void Solo ()
        {
            Register();
            
            if (transform.parent != null)
            {
                foreach (Transform item in transform.parent) 
                {
                    if (item == transform) continue;
                    DisplayObject displayObject = item.GetComponent<DisplayObject> ();
                    if (displayObject != null) displayObject.SetActive (false);
                }
                gameObject.SetActive (true);
            }else{
                foreach (var item in Resources.FindObjectsOfTypeAll<DisplayObject> ()) 
                {
                    if (item.transform.parent == null)
                    {
                        if (item == this)
                        {
                            item.SetActive (true);
                        }else{
                            item.SetActive (false);
                        }
                    }
                }
            }
        }
        
        
        
        
        public void HideAll ()
        {
            if (transform.parent != null)
            {
                foreach (Transform item in transform.parent) 
                {
                    if (item.GetComponent<DisplayObject> () != null) item.gameObject.SetActive (false);
                }
            }else{
                foreach (var item in Resources.FindObjectsOfTypeAll<DisplayObject> ()) 
                {
                    if (item.transform.parent == null) item.gameObject.SetActive (false);
                }
            }
        }
    }
}