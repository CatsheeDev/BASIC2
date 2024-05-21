








using UnityEngine;
using System.Collections.Generic;
using System;

namespace Pixelplacement
{
    public enum SplineDirection { Forward, Backwards }

    [ExecuteInEditMode]
    public class Spline : MonoBehaviour
    {
        
        public event Action OnSplineChanged;

        
        private class SplineReparam
        {
            
            public float length;
            public float percentage;

            
            public SplineReparam(float length, float percentage)
            {
                this.length = length;
                this.percentage = percentage;
            }
        }

        
        public Color color = Color.yellow;
        [Range(0, 1)] public float toolScale = .1f;
        public TangentMode defaultTangentMode;
        public SplineDirection direction;
        public bool loop;
        public SplineFollower[] followers;

        
        private SplineAnchor[] _anchors;
        private int _curveCount;
        private int _previousAnchorCount;
        private int _previousChildCount;
        private bool _wasLooping;
        private bool _previousLoopChoice;
        private bool _anchorsChanged;
        private SplineDirection _previousDirection;
        private float _curvePercentage = 0;
        private int _operatingCurve = 0;
        private float _currentCurve = 0;
        private int _previousLength;
        private int _slicesPerCurve = 10;
        private List<SplineReparam> _splineReparams = new List<SplineReparam>();
        private bool _lengthDirty = true;

        
        public float Length
        {
            get;
            private set;
        }

        public SplineAnchor[] Anchors
        {
            get
            {
                
                if (loop != _wasLooping)
                {
                    _previousAnchorCount = -1;
                    _wasLooping = loop;
                }

                if (!loop)
                {
                    if (transform.childCount != _previousAnchorCount || transform.childCount == 0)
                    {
                        _anchors = GetComponentsInChildren<SplineAnchor>();
                        _previousAnchorCount = transform.childCount;
                    }

                    return _anchors;
                }
                else
                {
                    if (transform.childCount != _previousAnchorCount || transform.childCount == 0)
                    {
                        
                        _anchors = GetComponentsInChildren<SplineAnchor>();
                        Array.Resize(ref _anchors, _anchors.Length + 1);
                        _anchors[_anchors.Length - 1] = _anchors[0];
                        _previousAnchorCount = transform.childCount;
                    }
                    return _anchors;
                }
            }
        }

        public Color SecondaryColor
        {
            get
            {
                Color secondaryColor = Color.Lerp(color, Color.black, .2f);
                return secondaryColor;
            }
        }

        
        void Reset()
        {
            
            if (Anchors.Length < 2)
            {
                AddAnchors(2 - Anchors.Length);
            }
        }

        
        void Update()
        {
            
            if (followers != null && followers.Length > 0 && Anchors.Length >= 2)
            {
                bool needToUpdate = false;

                
                if (_anchorsChanged || _previousChildCount != transform.childCount || direction != _previousDirection || loop != _previousLoopChoice)
                {
                    _previousChildCount = transform.childCount;
                    _previousLoopChoice = loop;
                    _previousDirection = direction;
                    _anchorsChanged = false;
                    needToUpdate = true;
                }

                
                for (int i = 0; i < followers.Length; i++)
                {
                    if (followers[i].WasMoved || needToUpdate)
                    {
                        followers[i].UpdateOrientation(this);
                    }
                }
            }

            
            bool anchorChanged = false;
            if (Anchors.Length > 1)
            {
                for (int i = 0; i < Anchors.Length; i++)
                {
                    
                    if (Anchors[i].Changed)
                    {
                        anchorChanged = true;
                        Anchors[i].Changed = false;
                        _anchorsChanged = true;
                    }

                    
                    if (!loop)
                    {
                        
                        if (i == 0)
                        {
                            Anchors[i].SetTangentStatus(false, true);
                            continue;
                        }

                        
                        if (i == Anchors.Length - 1)
                        {
                            Anchors[i].SetTangentStatus(true, false);
                            continue;
                        }

                        
                        Anchors[i].SetTangentStatus(true, true);

                    }
                    else
                    {
                        
                        Anchors[i].SetTangentStatus(true, true);
                    }
                }

            }

            
            if (_previousLength != Anchors.Length || anchorChanged)
            {
                HangleLengthChange();
                _previousLength = Anchors.Length;
            }
        }

        
        private void HangleLengthChange()
        {
            _lengthDirty = true;

            
            OnSplineChanged?.Invoke();
        }

        
        private float Reparam(float percent)
        {
            if (_lengthDirty) CalculateLength();

            
            for (int i = 0; i < _splineReparams.Count; i++)
            {
                float currentPercentage = _splineReparams[i].length / Length;

                if (currentPercentage == percent)
                {
                    return _splineReparams[i].percentage;
                }

                if (currentPercentage > percent)
                {
                    float fromP = _splineReparams[i - 1].length / Length;
                    float toP = currentPercentage;

                    
                    float maxAdjusted = toP - fromP;
                    float percentAdjusted = percent - fromP;

                    
                    float inBetweenPercentage = percentAdjusted / maxAdjusted;
                    float location = Mathf.Lerp(_splineReparams[i - 1].percentage, _splineReparams[i].percentage, inBetweenPercentage);

                    return location;
                }
            }

            return 0;
        }

        
        
        
        
        public void CalculateLength()
        {
            
            int totalSlices = (Anchors.Length - 1) * _slicesPerCurve;
            Length = 0;
            _splineReparams.Clear();
            
            
            _splineReparams.Add(new SplineReparam(0, 0));

            
            for (int i = 1; i < totalSlices + 1; i++)
            {
                
                float percent = i / (float)totalSlices;
                float previousPercent = (i - 1) / (float)totalSlices;

                
                Vector3 start = GetPosition(previousPercent, false);
                Vector3 end = GetPosition(percent, false);

                
                float distance = Vector3.Distance(start, end);
                Length += distance;

                
                _splineReparams.Add(new SplineReparam(Length, percent));
            }
            
            _lengthDirty = false;
            return;
        }

        
        
        
        public Vector3 Up(float percentage, bool normalized = true)
        {
            Quaternion lookRotation = Quaternion.LookRotation(GetDirection(percentage, normalized));
            return lookRotation * Vector3.up;
        }

        
        
        
        public Vector3 Right(float percentage, bool normalized = true)
        {
            Quaternion lookRotation = Quaternion.LookRotation(GetDirection(percentage, normalized));
            return lookRotation * Vector3.right;
        }

        
        
        
        public Vector3 Forward(float percentage, bool normalized = true)
        {
            return GetDirection(percentage, normalized);
        }

        
        
        
        public Vector3 GetDirection(float percentage, bool normalized = true)
        {
            if (normalized) percentage = Reparam(percentage);

            
            CurveDetail curveDetail = GetCurve(percentage);

            
            if (curveDetail.currentCurve < 0) return Vector3.zero;

            SplineAnchor startAnchor = Anchors[curveDetail.currentCurve];
            SplineAnchor endAnchor = Anchors[curveDetail.currentCurve + 1];
            return BezierCurves.GetFirstDerivative(startAnchor.Anchor.position, endAnchor.Anchor.position, startAnchor.OutTangent.position, endAnchor.InTangent.position, curveDetail.currentCurvePercentage).normalized;
        }

        
        
        
        public Vector3 GetPosition(float percentage, bool normalized = true)
        {
            if (normalized) percentage = Reparam(percentage);

            
            CurveDetail curveDetail = GetCurve(percentage);

            
            if (curveDetail.currentCurve < 0) return Vector3.zero;

            SplineAnchor startAnchor = Anchors[curveDetail.currentCurve];
            SplineAnchor endAnchor = Anchors[curveDetail.currentCurve + 1];
            return BezierCurves.GetPoint(startAnchor.Anchor.position, endAnchor.Anchor.position, startAnchor.OutTangent.position, endAnchor.InTangent.position, curveDetail.currentCurvePercentage, true, 100);
        }

        
        
        
        public Vector3 GetPosition(float percentage, Vector3 relativeOffset, bool normalized = true)
        {
            if (normalized) percentage = Reparam(percentage);

            
            Vector3 position = GetPosition(percentage);
            Quaternion lookRotation = Quaternion.LookRotation(GetDirection(percentage));

            
            Vector3 up = lookRotation * Vector3.up;
            Vector3 right = lookRotation * Vector3.right;
            Vector3 forward = lookRotation * Vector3.forward;

            
            Vector3 offset = position + right * relativeOffset.x;
            offset += up * relativeOffset.y;
            offset += forward * relativeOffset.z;

            return offset;
        }

        
        
        
        public float ClosestPoint(Vector3 point, int divisions = 100)
        {
            
            if (divisions <= 0) divisions = 1;

            
            float shortestDistance = float.MaxValue;
            Vector3 position = Vector3.zero;
            Vector3 offset = Vector3.zero;
            float closestPercentage = 0;
            float percentage = 0;
            float distance = 0;

            
            for (float i = 0; i < divisions + 1; i++)
            {
                percentage = i / divisions;
                position = GetPosition(percentage);
                offset = position - point;
                distance = offset.sqrMagnitude;

                
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestPercentage = percentage;
                }
            }

            return closestPercentage;
        }
        
        
        
        
        public GameObject[] AddAnchors(int count)
        {
            
            GameObject anchorTemplate = Resources.Load("Anchor") as GameObject;

            
            GameObject[] returnObjects = new GameObject[count];

            for (int i = 0; i < count; i++)
            {
                
                Transform previousPreviousAnchor = null;
                Transform previousAnchor = null;
                if (Anchors.Length == 1)
                {
                    previousPreviousAnchor = transform;
                    previousAnchor = Anchors[0].transform;
                }
                else if (Anchors.Length > 1)
                {
                    previousPreviousAnchor = Anchors[Anchors.Length - 2].transform;
                    previousAnchor = Anchors[Anchors.Length - 1].transform;
                }

                
                GameObject newAnchor = Instantiate<GameObject>(anchorTemplate);
                newAnchor.name = newAnchor.name.Replace("(Clone)", "");
                SplineAnchor anchor = newAnchor.GetComponent<SplineAnchor>();
                anchor.tangentMode = defaultTangentMode;
                newAnchor.transform.parent = transform;
                newAnchor.transform.rotation = Quaternion.LookRotation(transform.forward);

                
                
                anchor.InTangent.Translate(Vector3.up * .5f);
                anchor.OutTangent.Translate(Vector3.up * -.5f);

                
                if (previousPreviousAnchor != null && previousAnchor != null)
                {
                    
                    Vector3 direction = (previousAnchor.position - previousPreviousAnchor.position).normalized;
                    if (direction == Vector3.zero) direction = transform.forward;

                    
                    newAnchor.transform.position = previousAnchor.transform.position + (direction * 1.5f);
                }
                else
                {
                    newAnchor.transform.localPosition = Vector3.zero;
                }

                
                returnObjects[i] = newAnchor;
            }

            return returnObjects;
        }

        
        
        
        public CurveDetail GetCurve(float percentage)
        {
            
            if (loop)
            {
                percentage = Mathf.Repeat(percentage, 1);
            }
            else
            {
                percentage = Mathf.Clamp01(percentage);
            }

            
            if (Anchors.Length == 2)
            {
                
                if (direction == SplineDirection.Backwards)
                {
                    percentage = 1 - percentage;
                }

                
                return new CurveDetail(0, percentage);
            }
            else
            {
                
                _curveCount = Anchors.Length - 1;
                _currentCurve = _curveCount * percentage;

                if ((int)_currentCurve == _curveCount)
                {
                    _currentCurve = _curveCount - 1;
                    _curvePercentage = 1;
                }
                else
                {
                    _curvePercentage = _currentCurve - (int)_currentCurve;
                }

                _currentCurve = (int)_currentCurve;
                _operatingCurve = (int)_currentCurve;

                
                if (direction == SplineDirection.Backwards)
                {
                    _curvePercentage = 1 - _curvePercentage;
                    _operatingCurve = (_curveCount - 1) - _operatingCurve;
                }

                return new CurveDetail(_operatingCurve, _curvePercentage);
            }
        }
    }
}