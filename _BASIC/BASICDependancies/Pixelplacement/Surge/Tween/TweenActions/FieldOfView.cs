





using UnityEngine;
using System;
using Pixelplacement;

namespace Pixelplacement.TweenSystem
{
    class FieldOfView : TweenBase
    {
        
        public float EndValue {get; private set;}

        
        Camera _target;
        float _start;

        
        public FieldOfView (Camera target, float endValue, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.LoopType loop, Action startCallback, Action completeCallback)
        {
            
            SetEssentials (Tween.TweenType.FieldOfView, target.GetInstanceID (), duration, delay, obeyTimescale, curve, loop, startCallback, completeCallback);

            
            _target = target;
            EndValue = endValue;
        }

        
        protected override bool SetStartValue ()
        {
            if (_target == null) return false;
            _start = _target.fieldOfView;
            return true;
        }

        protected override void Operation (float percentage)
        {
            float calculatedValue = TweenUtilities.LinearInterpolate (_start, EndValue, percentage);
            _target.fieldOfView = calculatedValue;
        }

        
        public override void Loop ()
        {
            ResetStartTime ();
            _target.fieldOfView = _start;
        }

        public override void PingPong ()
        {
            ResetStartTime ();
            _target.fieldOfView = EndValue;
            EndValue = _start;
            _start = _target.fieldOfView;
        }
    }
}