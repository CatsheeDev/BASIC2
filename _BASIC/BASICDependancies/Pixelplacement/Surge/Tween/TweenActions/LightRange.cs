





using UnityEngine;
using System;
using Pixelplacement;

namespace Pixelplacement.TweenSystem
{
    class LightRange : TweenBase
    {
        
        public float EndValue {get; private set;}

        
        Light _target;
        float _start;

        
        public LightRange (Light target, float endValue, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.LoopType loop, Action startCallback, Action completeCallback)
        {
            
            SetEssentials (Tween.TweenType.LightRange, target.GetInstanceID (), duration, delay, obeyTimescale, curve, loop, startCallback, completeCallback);

            
            _target = target;
            EndValue = endValue;
        }

        
        protected override bool SetStartValue ()
        {
            if (_target == null) return false;
            _start = _target.range;
            return true;
        }

        protected override void Operation (float percentage)
        {
            float calculatedValue = TweenUtilities.LinearInterpolate (_start, EndValue, percentage);
            _target.range = calculatedValue;
        }

        
        public override void Loop ()
        {
            ResetStartTime ();
            _target.range = _start;
        }

        public override void PingPong ()
        {
            ResetStartTime ();
            _target.range = EndValue;
            EndValue = _start;
            _start = _target.range;
        }
    }
}