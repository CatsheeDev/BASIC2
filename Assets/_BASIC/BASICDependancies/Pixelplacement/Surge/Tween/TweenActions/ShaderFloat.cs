





using UnityEngine;
using System;
using Pixelplacement;

namespace Pixelplacement.TweenSystem
{
    class ShaderFloat : TweenBase
    {
        
        public float EndValue {get; private set;}

        
        Material _target;
        float _start;
        string _propertyName;
        
        
        public ShaderFloat (Material target, string propertyName, float endValue, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.LoopType loop, Action startCallback, Action completeCallback)
        {
            
            SetEssentials (Tween.TweenType.ShaderFloat, target.GetInstanceID (), duration, delay, obeyTimescale, curve, loop, startCallback, completeCallback);
            
            
            _target = target;
            _propertyName = propertyName;
            EndValue = endValue;
        }
        
        
        protected override bool SetStartValue ()
        {
            _start = _target.GetFloat (_propertyName);
            if (_target == null) return false;
            return true;
        }

        protected override void Operation (float percentage)
        {
            float calculatedValue = TweenUtilities.LinearInterpolate (_start, EndValue, percentage);
            _target.SetFloat (_propertyName, calculatedValue);
        }
        
        
        public override void Loop ()
        {
            ResetStartTime ();
            _target.SetFloat (_propertyName, _start);
        }
        
        public override void PingPong ()
        {
            ResetStartTime ();
            _target.SetFloat (_propertyName, EndValue);
            EndValue = _start;
            _start = _target.GetFloat (_propertyName);
        }
    }
}