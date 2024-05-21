





using UnityEngine;
using System;
using UnityEngine.UI;
using Pixelplacement;

namespace Pixelplacement.TweenSystem
{
    class AnchoredPosition : TweenBase
    {
        
        public Vector2 EndValue {get; private set;}

        
        RectTransform _target;
        Vector2 _start;

        
        public AnchoredPosition (RectTransform target, Vector2 endValue, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.LoopType loop, Action startCallback, Action completeCallback)
        {
            
            SetEssentials (Tween.TweenType.AnchoredPosition, target.GetInstanceID (), duration, delay, obeyTimescale, curve, loop, startCallback, completeCallback);

            
            _target = target;
            EndValue = endValue;
        }

        
        protected override bool SetStartValue ()
        {
            if (_target == null) return false;
            _start = _target.anchoredPosition;
            return true;
        }

        protected override void Operation (float percentage)
        {
            Vector3 calculatedValue = TweenUtilities.LinearInterpolate (_start, EndValue, percentage);
            _target.anchoredPosition = calculatedValue;
        }

        
        public override void Loop ()
        {
            ResetStartTime ();
            _target.anchoredPosition = _start;
        }

        public override void PingPong ()
        {
            ResetStartTime ();
            _target.anchoredPosition = EndValue;
            EndValue = _start;
            _start = _target.anchoredPosition;
        }
    }
}