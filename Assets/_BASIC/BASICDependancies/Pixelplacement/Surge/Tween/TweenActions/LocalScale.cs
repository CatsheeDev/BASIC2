





using UnityEngine;
using System;
using Pixelplacement;

namespace Pixelplacement.TweenSystem
{
    class LocalScale : TweenBase
    {
        
        public Vector3 EndValue {get; private set;}

        
        Transform _target;
        Vector3 _start;

        
        public LocalScale (Transform target, Vector3 endValue, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.LoopType loop, Action startCallback, Action completeCallback)
        {
            
            SetEssentials (Tween.TweenType.LocalScale, target.GetInstanceID (), duration, delay, obeyTimescale, curve, loop, startCallback, completeCallback);

            
            _target = target;
            EndValue = endValue;
        }

        
        protected override bool SetStartValue ()
        {
            if (_target == null) return false;
            _start = _target.localScale;
            return true;
        }

        protected override void Operation (float percentage)
        {
            Vector3 calculatedValue = TweenUtilities.LinearInterpolate (_start, EndValue, percentage);
            _target.localScale = calculatedValue;
        }

        
        public override void Loop ()
        {
            ResetStartTime ();
            _target.localScale = _start;
        }

        public override void PingPong ()
        {
            ResetStartTime ();
            _target.localScale = EndValue;
            EndValue = _start;
            _start = _target.localScale;
        }
    }
}