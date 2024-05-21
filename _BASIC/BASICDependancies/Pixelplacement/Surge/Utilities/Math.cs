








using UnityEngine;

namespace Pixelplacement
{
    public static class CoreMath  
    {
        
        
        
        
        
        
        
        public static float LinearInterpolate (float from, float to, float percentage)
        {
            return (to - from) * percentage + from;
        }

        
        
        
        
        
        
        public static Vector2 LinearInterpolate (Vector2 from, Vector2 to, float percentage)
        {
            return new Vector2 (LinearInterpolate (from.x, to.x, percentage), LinearInterpolate (from.y, to.y, percentage));
        }

        
        
        
        
        
        
        public static Vector3 LinearInterpolate (Vector3 from, Vector3 to, float percentage)
        {
            return new Vector3 (LinearInterpolate (from.x, to.x, percentage), LinearInterpolate (from.y, to.y, percentage), LinearInterpolate (from.z, to.z, percentage));
        }

        
        
        
        
        
        
        public static Vector4 LinearInterpolate (Vector4 from, Vector4 to, float percentage)
        {
            return new Vector4 (LinearInterpolate (from.x, to.x, percentage), LinearInterpolate (from.y, to.y, percentage), LinearInterpolate (from.z, to.z, percentage), LinearInterpolate (from.w, to.w, percentage));
        }

        
        
        
        
        
        
        public static Rect LinearInterpolate (Rect from, Rect to, float percentage)
        {
            return new Rect (LinearInterpolate (from.x, to.x, percentage), LinearInterpolate (from.y, to.y, percentage), LinearInterpolate (from.width, to.width, percentage), LinearInterpolate (from.height, to.height, percentage));
        }

        
        
        
        
        
        
        public static Color LinearInterpolate (Color from, Color to, float percentage)
        {
            return new Color (LinearInterpolate (from.r, to.r, percentage), LinearInterpolate (from.g, to.g, percentage), LinearInterpolate (from.b, to.b, percentage), LinearInterpolate (from.a, to.a, percentage));
        }

        
        
        
        
        
        
        
        public static float EvaluateCurve (AnimationCurve curve, float percentage)
        {
            return curve.Evaluate ((curve [curve.length - 1].time) * percentage);
        }
    }
}