





using UnityEngine;
using System;
using UnityEngine.UI;
using Pixelplacement;

namespace Pixelplacement.TweenSystem
{
    class CameraBackgroundColor : TweenBase
    {
        
        public Color EndValue { get; private set; }

        
        Camera _target;
        Color _start;

        
        public CameraBackgroundColor(Camera target, Color endValue, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.LoopType loop, Action startCallback, Action completeCallback)
        {
            
            SetEssentials(Tween.TweenType.CameraBackgroundColor, target.GetInstanceID(), duration, delay, obeyTimescale, curve, loop, startCallback, completeCallback);

            
            _target = target;
            EndValue = endValue;
        }

        
        protected override bool SetStartValue()
        {
            if (_target == null) return false;
            _start = _target.backgroundColor;
            return true;
        }

        protected override void Operation(float percentage)
        {
            Color calculatedValue = TweenUtilities.LinearInterpolate(_start, EndValue, percentage);
            _target.backgroundColor = calculatedValue;
        }

        
        public override void Loop()
        {
            ResetStartTime();
            _target.backgroundColor = _start;
        }

        public override void PingPong()
        {
            ResetStartTime();
            _target.backgroundColor = EndValue;
            EndValue = _start;
            _start = _target.backgroundColor;
        }
    }
}