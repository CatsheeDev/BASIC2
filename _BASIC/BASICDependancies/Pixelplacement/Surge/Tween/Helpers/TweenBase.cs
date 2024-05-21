








using UnityEngine;
using System;
using Pixelplacement;

#pragma warning disable 0168

namespace Pixelplacement.TweenSystem
{
    public abstract class TweenBase
    {
        
        public int targetInstanceID;
        public Tween.TweenType tweenType;

        
        public Tween.TweenStatus Status {get; private set;}
        public float Duration {get; private set;}
        public AnimationCurve Curve {get; private set;}
        public Keyframe[] CurveKeys { get; private set; }
        public bool ObeyTimescale {get; private set;}
        public Action StartCallback {get; private set;}
        public Action CompleteCallback {get; private set;}
        public float Delay {get; private set;}
        public Tween.LoopType LoopType {get; private set;}
        public float Percentage {get; private set; }

        
        protected float elapsedTime = 0.0f;

        
        
        
        
        public void Stop ()
        {
            Status = Tween.TweenStatus.Stopped;
            Tick ();
        }

        
        
        
        public void Start ()
        {
            elapsedTime = 0.0f;

            if (Status == Tween.TweenStatus.Canceled || Status == Tween.TweenStatus.Finished || Status == Tween.TweenStatus.Stopped)
            {
                Status = Tween.TweenStatus.Running;
                Operation (0);
                Tween.Instance.ExecuteTween (this);
            }
        }

        
        
        
        public void Resume()
        {
            if (Status != Tween.TweenStatus.Stopped) return;
            
            if (Status == Tween.TweenStatus.Stopped)
            {
                Status = Tween.TweenStatus.Running;
                Tween.Instance.ExecuteTween(this);
            }
        }

        
        
        
        public void Rewind ()
        {
            Cancel ();
            Operation (0);
        }

        
        
        
        public void Cancel ()
        {
            Status = Tween.TweenStatus.Canceled;
            Tick ();
        }

        
        
        
        public void Finish ()
        {
            Status = Tween.TweenStatus.Finished;
            Tick ();
        }

        
        
        
        public void Tick ()
        {
            
            if (Status == Tween.TweenStatus.Stopped) 
            {
                CleanUp();
                return;
            }

            
            if (Status == Tween.TweenStatus.Canceled)
            {
                Operation (0);
                Percentage = 0;
                CleanUp();
                return;
            }

            
            if (Status == Tween.TweenStatus.Finished) 
            {
                Operation (1);
                Percentage = 1;
                if (CompleteCallback != null) CompleteCallback ();
                CleanUp();
                return;
            }

            float progress = 0.0f;
            
            
            if (ObeyTimescale) 
            {
                elapsedTime += Time.deltaTime;
            }else{
                elapsedTime += Time.unscaledDeltaTime;
            }
            progress = Math.Max(elapsedTime, 0f);

            
            float percentage = Mathf.Min(progress / Duration, 1);
  
            
            if (percentage == 0 && Status != Tween.TweenStatus.Delayed) Status = Tween.TweenStatus.Delayed;

            
            if (percentage > 0 && Status == Tween.TweenStatus.Delayed) 
            {
                if (SetStartValue ())
                {
                    if (StartCallback != null) StartCallback ();
                    Status = Tween.TweenStatus.Running;	
                }else{
                    CleanUp();
                    return;
                }
            }

            
            float curveValue = percentage;

            
            if (Curve != null && CurveKeys.Length > 0) curveValue = TweenUtilities.EvaluateCurve (Curve, percentage);
        
            
            if (Status == Tween.TweenStatus.Running) 
            {
                try {
                    Operation (curveValue);
                    Percentage = curveValue;
                } catch (Exception ex) {
                    CleanUp();
                    return;
                }
            }

            
            if (percentage == 1)
            {
                if (CompleteCallback != null)
                {
                    CompleteCallback();
                }

                switch (LoopType) 
                {
                case Tween.LoopType.Loop:
                    Loop ();
                    break;

                case Tween.LoopType.PingPong:
                    PingPong ();
                    break;

                default:
                    Status = Tween.TweenStatus.Finished;
                    CleanUp();
                    return;
                }
            }
        }
        
        
        private void CleanUp()
        {
            if (Tween.activeTweens.Contains(this))
            {
                Tween.activeTweens.Remove(this);
            }
        }

        
        
        
        
        protected void ResetStartTime ()
        {
            elapsedTime = -Delay;
        }

        
        
        
        protected void SetEssentials (Tween.TweenType tweenType, int targetInstanceID, float duration, float delay, bool obeyTimeScale, AnimationCurve curve, Tween.LoopType loop, Action startCallback, Action completeCallback)
        {
            this.tweenType = tweenType;
            this.targetInstanceID = targetInstanceID;

            if (delay > 0)
                Status = Tween.TweenStatus.Delayed;

            Duration = duration;
            Delay = delay;
            Curve = curve;

            CurveKeys = curve == null ? null : curve.keys;
            StartCallback = startCallback;
            CompleteCallback = completeCallback;
            LoopType = loop;
            ObeyTimescale = obeyTimeScale;

            ResetStartTime ();
        }

        
        
        
        
        protected abstract bool SetStartValue ();

        
        
        
        protected abstract void Operation (float percentage);

        
        
        
        public abstract void Loop ();

        
        
        
        public abstract void PingPong ();
    }
}