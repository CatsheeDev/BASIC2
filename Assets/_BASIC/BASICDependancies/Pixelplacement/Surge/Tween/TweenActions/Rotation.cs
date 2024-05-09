





using UnityEngine;
using System;
using Pixelplacement;

namespace Pixelplacement.TweenSystem
{
    class Rotation : TweenBase
    {
        
        public Vector3 EndValue {get; private set;}

        
        Transform _target;
        Vector3 _start;

        
        public Rotation (Transform target, Vector3 endValue, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.LoopType loop, Action startCallback, Action completeCallback)
        {
            
            SetEssentials (Tween.TweenType.Rotation, target.GetInstanceID (), duration, delay, obeyTimescale, curve, loop, startCallback, completeCallback);

            
            _target = target;
            EndValue = endValue;
        }

        
        protected override bool SetStartValue ()
        {
            if (_target == null) return false;
            _start = _target.eulerAngles;
            return true;
        }

        protected override void Operation (float percentage)
        {
            Quaternion calculatedValue = Quaternion.Euler (TweenUtilities.LinearInterpolateRotational (_start, EndValue, percentage));
            _target.rotation = calculatedValue;
        }

        
        public override void Loop ()
        {
            ResetStartTime ();
            _target.eulerAngles = _start;
        }

        public override void PingPong ()
        {
            ResetStartTime ();
            _target.eulerAngles = EndValue;
            EndValue = _start;
            _start = _target.eulerAngles;
        }
    }
}