








using UnityEngine;
using System.Collections;
using Pixelplacement;

namespace Pixelplacement.TweenSystem
    {
    public class TweenEngine : MonoBehaviour
        {
            public void ExecuteTween(TweenBase tween) 
            {
                Tween.activeTweens.Add(tween);
            }
            
            private void Update()
            {
                foreach (var tween in Tween.activeTweens.ToArray())
                {
                    tween.Tick();
                }
                
                
                
                
                
                
                
                
            }
        }
    }