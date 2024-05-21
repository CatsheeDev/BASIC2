








using UnityEngine;
using System.Collections;

namespace Pixelplacement
{
    [System.Serializable]
    public class SplineFollower
    {
        
        public Transform target;
        public float percentage = -1;
        public bool faceDirection;

        
        public bool WasMoved
        {
            get
            {
                if (percentage != _previousPercentage || faceDirection != _previousFaceDirection) {
                    _previousPercentage = percentage;
                    _previousFaceDirection = faceDirection;
                    return true;
                } else {
                    return false;	
                }
            }
        }

        
        float _previousPercentage;
        bool _previousFaceDirection;
        bool _detached;

        
        public void UpdateOrientation (Spline spline)
        {
            if (target == null) return;

            
            if (!spline.loop) percentage = Mathf.Clamp01 (percentage);

            
            if (faceDirection)
            {
                if (spline.direction == SplineDirection.Forward)
                {
                    target.LookAt (target.position + spline.GetDirection (percentage));
                }else{
                    target.LookAt (target.position - spline.GetDirection (percentage));
                }
            }

            target.position = spline.GetPosition (percentage);
        }
    }
}