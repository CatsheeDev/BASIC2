








using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelplacement
{
    public struct CurveDetail
    {
        
        public int currentCurve;
        public float currentCurvePercentage;

        
        public CurveDetail (int currentCurve, float currentCurvePercentage)
        {
            this.currentCurve = currentCurve;
            this.currentCurvePercentage = currentCurvePercentage;
        }
    }
}