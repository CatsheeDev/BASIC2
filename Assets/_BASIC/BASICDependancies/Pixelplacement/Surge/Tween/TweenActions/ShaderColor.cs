





using UnityEngine;
using System;
using Pixelplacement;

namespace Pixelplacement.TweenSystem
{
    class ShaderColor : TweenBase
    {
        
        public Color EndValue {get; private set;}

        
        Material _target;
        Color _start;
        string _propertyName;
        
        
        public ShaderColor (Material target, string propertyName, Color endValue, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.LoopType loop, Action startCallback, Action completeCallback)
        {
            
            SetEssentials (Tween.TweenType.ShaderColor, target.GetInstanceID (), duration, delay, obeyTimescale, curve, loop, startCallback, completeCallback);
            
            
            _target = target;
            _propertyName = propertyName;
            EndValue = endValue;
        }
        
        
        protected override bool SetStartValue ()
        {
            _start = _target.GetColor (_propertyName);
            if (_target == null) return false;
            return true;
        }

        protected override void Operation (float percentage)
        {
            Color calculatedValue = TweenUtilities.LinearInterpolate (_start, EndValue, percentage);
            _target.SetColor (_propertyName, calculatedValue);
        }
        
        
        public override void Loop ()
        {
            ResetStartTime ();
            _target.SetColor (_propertyName, _start);
        }
        
        public override void PingPong ()
        {
            ResetStartTime ();
            _target.SetColor (_propertyName, EndValue);
            EndValue = _start;
            _start = _target.GetColor (_propertyName);
        }
    }
}