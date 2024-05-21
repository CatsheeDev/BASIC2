﻿





using UnityEngine;
using System;
using Pixelplacement;

namespace Pixelplacement.TweenSystem
{
    class ValueColor : TweenBase
    {
        
        public Color EndValue {get; private set;}

        
        Action<Color> _valueUpdatedCallback;
        Color _start;

        
        public ValueColor (Color startValue, Color endValue, Action<Color> valueUpdatedCallback, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.LoopType loop, Action startCallback, Action completeCallback)
        {
            
            SetEssentials (Tween.TweenType.Value, -1, duration, delay, obeyTimescale, curve, loop, startCallback, completeCallback);

            
            _valueUpdatedCallback = valueUpdatedCallback;
            _start = startValue;
            EndValue = endValue;
        }

        
        protected override bool SetStartValue ()
        {
            return true;
        }

        protected override void Operation (float percentage)
        {
            Color calculatedValue = TweenUtilities.LinearInterpolate (_start, EndValue, percentage);
            _valueUpdatedCallback (calculatedValue);
        }

        
        public override void Loop ()
        {
            ResetStartTime ();
        }

        public override void PingPong ()
        {
            ResetStartTime ();
            Color temp = _start;
            _start = EndValue;
            EndValue = temp;
        }
    }
}