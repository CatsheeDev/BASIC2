





using UnityEngine;
using System;
using UnityEngine.UI;

namespace Pixelplacement.TweenSystem
{
    class GraphicColor : TweenBase
    {
        
        public Color EndValue {get; private set;}

        
        Graphic _target;
        Color _start;

        
        public GraphicColor (Graphic target, Color endValue, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.LoopType loop, Action startCallback, Action completeCallback)
        {
            
            SetEssentials (Tween.TweenType.GraphicColor, target.GetInstanceID (), duration, delay, obeyTimescale, curve, loop, startCallback, completeCallback);

            
            _target = target;
            EndValue = endValue;
        }

        
        protected override bool SetStartValue ()
        {
            if (_target == null) return false;
            _start = _target.color;
            return true;
        }

        protected override void Operation (float percentage)
        {
            Color calculatedValue = TweenUtilities.LinearInterpolate (_start, EndValue, percentage);
            _target.color = calculatedValue;
        }

        
        public override void Loop ()
        {
            ResetStartTime ();
            _target.color = _start;
        }

        public override void PingPong ()
        {
            ResetStartTime ();
            _target.color = EndValue;
            EndValue = _start;
            _start = _target.color;
        }
    }
}