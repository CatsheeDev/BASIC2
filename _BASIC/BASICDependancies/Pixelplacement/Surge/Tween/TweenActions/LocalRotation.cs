





using UnityEngine;
using System;
using Pixelplacement;

namespace Pixelplacement.TweenSystem
{
    class LocalRotation : TweenBase
    {
        
        public Vector3 EndValue {get; private set;}

        
        Transform _target;
        Vector3 _start;

        
        public LocalRotation (Transform target, Vector3 endValue, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.LoopType loop, Action startCallback, Action completeCallback)
        {
            
            SetEssentials (Tween.TweenType.Rotation, target.GetInstanceID (), duration, delay, obeyTimescale, curve, loop, startCallback, completeCallback);

            
            _target = target;
            EndValue = endValue;
        }

        
        protected override bool SetStartValue ()
        {
            if (_target == null) return false;
            _start = _target.localEulerAngles;
            return true;
        }

        protected override void Operation (float percentage)
        {
            
            Quaternion calculatedValue = Quaternion.LerpUnclamped(Quaternion.Euler(_start), Quaternion.Euler(EndValue), percentage);
            _target.localRotation = calculatedValue;
        }

        
        public override void Loop ()
        {
            ResetStartTime ();
            _target.localEulerAngles = _start;
        }

        public override void PingPong ()
        {
            ResetStartTime ();
            _target.localEulerAngles = EndValue;
            EndValue = _start;
            _start = _target.localEulerAngles;
        }
    }
}