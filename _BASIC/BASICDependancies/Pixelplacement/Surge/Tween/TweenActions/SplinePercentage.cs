





using UnityEngine;
using System;
using Pixelplacement;

namespace Pixelplacement.TweenSystem
{
    class SplinePercentage : TweenBase
    {
        
        public float EndValue {get; private set;}

        
        Transform _target;
        Spline _spline;
        float _startPercentage;
        bool _faceDirection;

        
        public SplinePercentage (Spline spline, Transform target, float startPercentage, float endPercentage, bool faceDirection, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.LoopType loop, Action startCallback, Action completeCallback)
        {
            
            if (!spline.loop)
            {
                startPercentage = Mathf.Clamp01 (startPercentage);
                endPercentage = Mathf.Clamp01 (endPercentage);
            }

            
            SetEssentials (Tween.TweenType.Spline, target.GetInstanceID (), duration, delay, obeyTimescale, curve, loop, startCallback, completeCallback);

            
            _spline = spline;
            _target = target;
            EndValue = endPercentage;
            _startPercentage = startPercentage;
            _faceDirection = faceDirection;
        }

        
        protected override bool SetStartValue ()
        {
            if (_target == null) return false;
            return true;
        }

        protected override void Operation (float percentage)
        {
            float calculatedValue = TweenUtilities.LinearInterpolate (_startPercentage, EndValue, percentage);
            _target.position = _spline.GetPosition (calculatedValue);
            if (_faceDirection)
            {
                if (_spline.direction == SplineDirection.Forward)
                {
                    _target.LookAt (_target.position + _spline.GetDirection (calculatedValue));
                }else{
                    _target.LookAt (_target.position - _spline.GetDirection (calculatedValue));
                }
            }
        }

        
        public override void Loop ()
        {
            ResetStartTime ();
            _target.position = _spline.GetPosition (_startPercentage);
        }

        public override void PingPong ()
        {
            ResetStartTime ();
            float temp = EndValue;
            EndValue = _startPercentage;
            _startPercentage = temp;
        }
    }
}