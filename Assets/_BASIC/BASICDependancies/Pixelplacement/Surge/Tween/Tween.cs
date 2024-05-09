








using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

namespace Pixelplacement
{
    public class Tween
    {
        
        
        
        public enum TweenType { Position, Rotation, LocalScale, LightColor, CameraBackgroundColor, LightIntensity, LightRange, FieldOfView, SpriteRendererColor, GraphicColor, AnchoredPosition, Size, Volume, Pitch, PanStereo, ShaderFloat, ShaderColor, ShaderInt, ShaderVector, Value, CanvasGroupAlpha, Spline, TextColor, ImageColor, RawImageColor, TextMeshColor };

        
        
        
        public enum LoopType { None, Loop, PingPong };

        
        
        
        public enum TweenStatus { Delayed, Running, Canceled, Stopped, Finished }

        
        public static TweenSystem.TweenEngine Instance
        {
            get
            {
                if (_instance == null) _instance = new GameObject("(Tween Engine)", typeof(TweenSystem.TweenEngine)).GetComponent<TweenSystem.TweenEngine>();
                GameObject.DontDestroyOnLoad(_instance.gameObject);
                
                return _instance;
            }
        }

        
        public static List<TweenSystem.TweenBase> activeTweens = new List<TweenSystem.TweenBase>();

        
        private static TweenSystem.TweenEngine _instance;
        private static AnimationCurve _easeIn;
        private static AnimationCurve _easeInStrong;
        private static AnimationCurve _easeOut;
        private static AnimationCurve _easeOutStrong;
        private static AnimationCurve _easeInOut;
        private static AnimationCurve _easeInOutStrong;
        private static AnimationCurve _easeInBack;
        private static AnimationCurve _easeOutBack;
        private static AnimationCurve _easeInOutBack;
        private static AnimationCurve _easeSpring;
        private static AnimationCurve _easeBounce;
        private static AnimationCurve _easeWobble;

        
        
        
        
        
        public static TweenSystem.TweenBase Shake(Transform target, Vector3 initialPosition, Vector3 intensity, float duration, float delay, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.ShakePosition tween = new TweenSystem.ShakePosition(target, initialPosition, intensity, duration, delay, EaseLinear, startCallback, completeCallback, loop, obeyTimescale);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Spline(Spline spline, Transform target, float startPercentage, float endPercentage, bool faceDirection, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.SplinePercentage tween = new TweenSystem.SplinePercentage(spline, target, startPercentage, endPercentage, faceDirection, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase CanvasGroupAlpha(CanvasGroup target, float endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.CanvasGroupAlpha tween = new TweenSystem.CanvasGroupAlpha(target, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase CanvasGroupAlpha(CanvasGroup target, float startValue, float endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.alpha = startValue;
            return CanvasGroupAlpha(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase Value(Rect startValue, Rect endValue, Action<Rect> valueUpdatedCallback, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.ValueRect tween = new TweenSystem.ValueRect(startValue, endValue, valueUpdatedCallback, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Value(Vector4 startValue, Vector4 endValue, Action<Vector4> valueUpdatedCallback, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.ValueVector4 tween = new TweenSystem.ValueVector4(startValue, endValue, valueUpdatedCallback, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Value(Vector3 startValue, Vector3 endValue, Action<Vector3> valueUpdatedCallback, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.ValueVector3 tween = new TweenSystem.ValueVector3(startValue, endValue, valueUpdatedCallback, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Value(Vector2 startValue, Vector2 endValue, Action<Vector2> valueUpdatedCallback, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.ValueVector2 tween = new TweenSystem.ValueVector2(startValue, endValue, valueUpdatedCallback, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Value(Color startValue, Color endValue, Action<Color> valueUpdatedCallback, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.ValueColor tween = new TweenSystem.ValueColor(startValue, endValue, valueUpdatedCallback, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Value(int startValue, int endValue, Action<int> valueUpdatedCallback, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.ValueInt tween = new TweenSystem.ValueInt(startValue, endValue, valueUpdatedCallback, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Value(float startValue, float endValue, Action<float> valueUpdatedCallback, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.ValueFloat tween = new TweenSystem.ValueFloat(startValue, endValue, valueUpdatedCallback, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase ShaderVector(Material target, string propertyName, Vector4 endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.ShaderVector tween = new TweenSystem.ShaderVector(target, propertyName, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase ShaderVector(Material target, string propertyName, Vector4 startValue, Vector4 endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.SetVector(propertyName, startValue);
            return ShaderVector(target, propertyName, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase ShaderInt(Material target, string propertyName, int endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.ShaderInt tween = new TweenSystem.ShaderInt(target, propertyName, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase ShaderInt(Material target, string propertyName, int startValue, int endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.SetInt(propertyName, startValue);
            return ShaderInt(target, propertyName, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase ShaderColor(Material target, string propertyName, Color endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.ShaderColor tween = new TweenSystem.ShaderColor(target, propertyName, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase ShaderColor(Material target, string propertyName, Color startValue, Color endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.SetColor(propertyName, startValue);
            return ShaderColor(target, propertyName, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase ShaderFloat(Material target, string propertyName, float endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.ShaderFloat tween = new TweenSystem.ShaderFloat(target, propertyName, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase ShaderFloat(Material target, string propertyName, float startValue, float endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.SetFloat(propertyName, startValue);
            return ShaderFloat(target, propertyName, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase Pitch(AudioSource target, float endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.Pitch tween = new TweenSystem.Pitch(target, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Pitch(AudioSource target, float startValue, float endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.pitch = startValue;
            return Pitch(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase PanStereo(AudioSource target, float endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.PanStereo tween = new TweenSystem.PanStereo(target, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase PanStereo(AudioSource target, float startValue, float endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.panStereo = startValue;
            return PanStereo(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase Volume(AudioSource target, float endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.Volume tween = new TweenSystem.Volume(target, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Volume(AudioSource target, float startValue, float endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.volume = startValue;
            return Volume(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase Size(RectTransform target, Vector2 endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.Size tween = new TweenSystem.Size(target, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Size(RectTransform target, Vector2 startValue, Vector2 endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.sizeDelta = startValue;
            return Size(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase FieldOfView(Camera target, float endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.FieldOfView tween = new TweenSystem.FieldOfView(target, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase FieldOfView(Camera target, float startValue, float endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.fieldOfView = startValue;
            return FieldOfView(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase LightRange(Light target, float endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.LightRange tween = new TweenSystem.LightRange(target, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase LightRange(Light target, float startValue, float endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.range = startValue;
            return LightRange(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase LightIntensity(Light target, float endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.LightIntensity tween = new TweenSystem.LightIntensity(target, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase LightIntensity(Light target, float startValue, float endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.intensity = startValue;
            return LightIntensity(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase LocalScale(Transform target, Vector3 endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.LocalScale tween = new TweenSystem.LocalScale(target, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase LocalScale(Transform target, Vector3 startValue, Vector3 endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.localScale = startValue;
            return LocalScale(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }
        
        
        
        
        public static TweenSystem.TweenBase Color(Graphic target, Color endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.GraphicColor tween = new TweenSystem.GraphicColor(target, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Color(Graphic target, Color startValue, Color endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.color = startValue;
            return Color(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }        
        
        
        
        
        public static TweenSystem.TweenBase Color(Light target, Color endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.LightColor tween = new TweenSystem.LightColor(target, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Color(Light target, Color startValue, Color endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.color = startValue;
            return Color(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase Color(Material target, Color endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.ShaderColor tween = new TweenSystem.ShaderColor(target, "_Color", endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Color(Material target, Color startColor, Color endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.color = startColor;
            return Color(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        
        public static TweenSystem.TweenBase Color(Renderer target, Color endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            Material material = target.material;
            return Color(material, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        
        public static TweenSystem.TweenBase Color(Renderer target, Color startColor, Color endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            Material material = target.material;
            return Color(material, startColor, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase Color(SpriteRenderer target, Color endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.SpriteRendererColor tween = new TweenSystem.SpriteRendererColor(target, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Color(SpriteRenderer target, Color startColor, Color endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.color = startColor;
            return Color(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase Color(Camera target, Color endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.CameraBackgroundColor tween = new TweenSystem.CameraBackgroundColor(target, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Color(Camera target, Color startColor, Color endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.backgroundColor = startColor;
            return Color(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase Position(Transform target, Vector3 endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.Position tween = new TweenSystem.Position(target, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Position(Transform target, Vector3 startValue, Vector3 endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.position = startValue;
            return Position(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase AnchoredPosition(RectTransform target, Vector2 endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.AnchoredPosition tween = new TweenSystem.AnchoredPosition(target, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase AnchoredPosition(RectTransform target, Vector2 startValue, Vector2 endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.anchoredPosition = startValue;
            return AnchoredPosition(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase LocalPosition(Transform target, Vector3 endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.LocalPosition tween = new TweenSystem.LocalPosition(target, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase LocalPosition(Transform target, Vector3 startValue, Vector3 endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.localPosition = startValue;
            return LocalPosition(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase Rotate(Transform target, Vector3 amount, Space space, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.Rotate tween = new TweenSystem.Rotate(target, amount, space, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Rotation(Transform target, Vector3 endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            endValue = Quaternion.Euler(endValue).eulerAngles;
            TweenSystem.Rotation tween = new TweenSystem.Rotation(target, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Rotation(Transform target, Vector3 startValue, Vector3 endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            startValue = Quaternion.Euler(startValue).eulerAngles;
            endValue = Quaternion.Euler(endValue).eulerAngles;
            target.rotation = Quaternion.Euler(startValue);
            return Rotation(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase Rotation(Transform target, Quaternion endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.Rotation tween = new TweenSystem.Rotation(target, endValue.eulerAngles, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase Rotation(Transform target, Quaternion startValue, Quaternion endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.rotation = startValue;
            return Rotation(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase LocalRotation(Transform target, Vector3 endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            endValue = Quaternion.Euler(endValue).eulerAngles;
            TweenSystem.LocalRotation tween = new TweenSystem.LocalRotation(target, endValue, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase LocalRotation(Transform target, Vector3 startValue, Vector3 endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            startValue = Quaternion.Euler(startValue).eulerAngles;
            endValue = Quaternion.Euler(endValue).eulerAngles;
            target.localRotation = Quaternion.Euler(startValue);
            return LocalRotation(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase LocalRotation(Transform target, Quaternion endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            TweenSystem.LocalRotation tween = new TweenSystem.LocalRotation(target, endValue.eulerAngles, duration, delay, obeyTimescale, easeCurve, loop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        
        
        
        public static TweenSystem.TweenBase LocalRotation(Transform target, Quaternion startValue, Quaternion endValue, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.localRotation = startValue;
            return LocalRotation(target, endValue, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase LookAt(Transform target, Transform targetToLookAt, Vector3 up, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            Vector3 direction = targetToLookAt.position - target.position;
            Quaternion rotation = Quaternion.LookRotation(direction, up);
            return Rotation(target, rotation, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        
        
        
        public static TweenSystem.TweenBase LookAt(Transform target, Vector3 positionToLookAt, Vector3 up, float duration, float delay, AnimationCurve easeCurve = null, LoopType loop = LoopType.None, Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            Vector3 direction = positionToLookAt - target.position;
            Quaternion rotation = Quaternion.LookRotation(direction, up);
            return Rotation(target, rotation, duration, delay, easeCurve, loop, startCallback, completeCallback, obeyTimescale);
        }

        

        
        
        
        public static void Stop(int targetInstanceID, TweenType tweenType)
        {
            if (targetInstanceID == -1) return;
            for (int i = 0; i < activeTweens.Count; i++)
            {
                if (activeTweens[i].targetInstanceID == targetInstanceID && activeTweens[i].tweenType == tweenType && activeTweens[i].Status != TweenStatus.Delayed)
                {
                    activeTweens[i].Stop();
                }
            }
        }

        
        
        
        public static void Stop(int targetInstanceID)
        {
            StopInstanceTarget(targetInstanceID);
        }

        
        
        
        public static void StopAll()
        {
            foreach (TweenSystem.TweenBase item in activeTweens)
            {
                item.Stop();
            }
        }

        
        
        
        public static void FinishAll()
        {
            foreach (TweenSystem.TweenBase item in activeTweens)
            {
                item.Finish();
            }
        }

        
        
        
        public static void Finish(int targetInstanceID)
        {
            FinishInstanceTarget(targetInstanceID);
        }

        
        
        
        public static void Cancel(int targetInstanceID)
        {
            CancelInstanceTarget(targetInstanceID);
        }

        
        
        
        public static void CancelAll()
        {
            foreach (TweenSystem.TweenBase item in activeTweens)
            {
                item.Cancel();
            }
        }

        
        
        
        
        public static AnimationCurve EaseLinear
        {
            get { return null; }
        }

        
        
        
        public static AnimationCurve EaseIn
        {
            get
            {
                if(_easeIn == null) _easeIn = new AnimationCurve(new Keyframe(0, 0, 0, 0), new Keyframe(1, 1, 1, 0));
                return _easeIn;
            }
        }

        
        
        
        public static AnimationCurve EaseInStrong
        {
            get
            {
                if (_easeInStrong == null) _easeInStrong = new AnimationCurve(new Keyframe(0, 0, .03f, .03f), new Keyframe(0.45f, 0.03f, 0.2333333f, 0.2333333f), new Keyframe(0.7f, 0.13f, 0.7666667f, 0.7666667f), new Keyframe(0.85f, 0.3f, 2.233334f, 2.233334f), new Keyframe(0.925f, 0.55f, 4.666665f, 4.666665f), new Keyframe(1, 1, 5.999996f, 5.999996f));
                return _easeInStrong;
            }
        }

        
        
        
        public static AnimationCurve EaseOut
        {
            get
            {
                if (_easeOut == null) _easeOut = new AnimationCurve(new Keyframe(0, 0, 0, 1), new Keyframe(1, 1, 0, 0));
                return _easeOut;
            }
        }

        
        
        
        public static AnimationCurve EaseOutStrong
        {
            get
            {
                if(_easeOutStrong == null) _easeOutStrong = new AnimationCurve(new Keyframe(0, 0, 13.80198f, 13.80198f), new Keyframe(0.04670785f, 0.3973127f, 5.873408f, 5.873408f), new Keyframe(0.1421811f, 0.7066917f, 2.313627f, 2.313627f), new Keyframe(0.2483539f, 0.8539293f, 0.9141542f, 0.9141542f), new Keyframe(0.4751028f, 0.954047f, 0.264541f, 0.264541f), new Keyframe(1, 1, .03f, .03f));
                return _easeOutStrong;
            }
        }

        
        
        
        public static AnimationCurve EaseInOut
        {
            get
            {
                if(_easeInOut == null) _easeInOut = AnimationCurve.EaseInOut(0, 0, 1, 1);
                return _easeInOut;
            }
        }

        
        
        
        public static AnimationCurve EaseInOutStrong
        {
            get
            {
                if(_easeInOutStrong == null) _easeInOutStrong = new AnimationCurve(new Keyframe(0, 0, 0.03f, 0.03f), new Keyframe(0.5f, 0.5f, 3.257158f, 3.257158f), new Keyframe(1, 1, .03f, .03f));
                return _easeInOutStrong;
            }
        }

        
        
        
        public static AnimationCurve EaseInBack
        {
            get
            {
                if(_easeInBack == null) _easeInBack = new AnimationCurve(new Keyframe(0, 0, -1.1095f, -1.1095f), new Keyframe(1, 1, 2, 2));
                return _easeInBack;
            }
        }

        
        
        
        public static AnimationCurve EaseOutBack
        {
            get
            {
                if (_easeOutBack == null) _easeOutBack = new AnimationCurve(new Keyframe(0, 0, 2, 2), new Keyframe(1, 1, -1.1095f, -1.1095f));
                return _easeOutBack;
            }
        }

        
        
        
        public static AnimationCurve EaseInOutBack
        {
            get
            {
                if(_easeInOutBack == null) _easeInOutBack = new AnimationCurve(new Keyframe(1, 1, -1.754543f, -1.754543f), new Keyframe(0, 0, -1.754543f, -1.754543f));
                return _easeInOutBack;
            }
        }

        
        
        
        public static AnimationCurve EaseSpring
        {
            get
            {
                if(_easeSpring == null) _easeSpring = new AnimationCurve(new Keyframe(0f, -0.0003805831f, 8.990726f, 8.990726f), new Keyframe(0.35f, 1f, -4.303913f, -4.303913f), new Keyframe(0.55f, 1f, 1.554695f, 1.554695f), new Keyframe(0.7730452f, 1f, -2.007816f, -2.007816f), new Keyframe(1f, 1f, -1.23451f, -1.23451f));
                return _easeSpring;
            }
        }

        
        
        
        public static AnimationCurve EaseBounce
        {
            get
            {
                if(_easeBounce == null) _easeBounce = new AnimationCurve(new Keyframe(0, 0, 0, 0), new Keyframe(0.25f, 1, 11.73749f, -5.336508f), new Keyframe(0.4f, 0.6f, -0.1904764f, -0.1904764f), new Keyframe(0.575f, 1, 5.074602f, -3.89f), new Keyframe(0.7f, 0.75f, 0.001192093f, 0.001192093f), new Keyframe(0.825f, 1, 4.18469f, -2.657566f), new Keyframe(0.895f, 0.9f, 0, 0), new Keyframe(0.95f, 1, 3.196362f, -2.028364f), new Keyframe(1, 1, 2.258884f, 0.5f));
                return _easeBounce;
            }
        }

        
        
        
        public static AnimationCurve EaseWobble
        {
            get
            {
                if(_easeWobble == null) _easeWobble = new AnimationCurve(new Keyframe(0f, 0f, 11.01978f, 30.76278f), new Keyframe(0.08054394f, 1f, 0f, 0f), new Keyframe(0.3153235f, -0.75f, 0f, 0f), new Keyframe(0.5614113f, 0.5f, 0f, 0f), new Keyframe(0.75f, -0.25f, 0f, 0f), new Keyframe(0.9086903f, 0.1361611f, 0f, 0f), new Keyframe(1f, 0f, -4.159244f, -1.351373f));
                return _easeWobble;
            }
        }

        
        static void StopInstanceTarget(int id)
        {
            for (int i = 0; i < activeTweens.Count; i++)
            {
                if (activeTweens[i].targetInstanceID == id)
                {
                    activeTweens[i].Stop();
                }
            }
        }

        static void StopInstanceTargetType(int id, TweenType type)
        {
            for (int i = activeTweens.Count - 1; i >= 0; i--)
            {
                if (activeTweens[i].targetInstanceID == id && activeTweens[i].tweenType == type)
                {
                    activeTweens[i].Stop();
                }
            }
        }

        static void FinishInstanceTarget(int id)
        {
            for (int i = activeTweens.Count - 1; i >= 0; i--)
            {
                if (activeTweens[i].targetInstanceID == id)
                {
                    activeTweens[i].Finish();
                    
                }
            }
        }

        static void CancelInstanceTarget(int id)
        {
            for (int i = activeTweens.Count - 1; i >= 0; i--)
            {
                if (activeTweens[i].targetInstanceID == id)
                {
                    activeTweens[i].Cancel();
                    
                }
            }
        }

        static void SendTweenForProcessing(TweenSystem.TweenBase tween, bool interrupt = false)
        {
            if (!Application.isPlaying) 
            {
                
                return;
            }

            if (interrupt && tween.Delay == 0)
            {
                StopInstanceTargetType(tween.targetInstanceID, tween.tweenType);
            }

            Instance.ExecuteTween(tween);
        }
    }
}