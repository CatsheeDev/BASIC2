








using UnityEngine;
using System.Collections;

namespace Pixelplacement
{
    public enum TangentMode {Mirrored, Aligned, Free}

    [ExecuteInEditMode]
    public class SplineAnchor : MonoBehaviour
    {
        
        public TangentMode tangentMode;

        
        public bool RenderingChange
        {
            get; set;
        }

        public bool Changed
        {
            get; set;
        }

        public Transform Anchor
        {
            get
            {
                if (!_initialized) Initialize();
                return _anchor;
            }

            private set
            {
                _anchor = value;
            }
        }

        public Transform InTangent
        {
            get
            {
                if (!_initialized) Initialize();
                return _inTangent;
            }

            private set
            {
                _inTangent = value;
            }
        }

        public Transform OutTangent
        {
            get
            {
                if (!_initialized) Initialize();
                return _outTangent;
            }

            private set
            {
                _outTangent = value;
            }
        }

        
        bool _initialized;
        [SerializeField][HideInInspector] Transform _masterTangent;
        [SerializeField][HideInInspector] Transform _slaveTangent;
        TangentMode _previousTangentMode;
        Vector3 _previousInPosition;
        Vector3 _previousOutPosition;
        Vector3 _previousAnchorPosition;
        Bounds _skinnedBounds;
        Transform _anchor;
        Transform _inTangent;
        Transform _outTangent;

        
        void Awake ()
        {
            Initialize ();
        }

        
        void Update ()
        {
            
            transform.localScale = Vector3.one;

            
            if (!_initialized)
            {
                Initialize ();
            }

            
            Anchor.localPosition = Vector3.zero;

            
            if (_previousAnchorPosition != transform.position)
            {
                Changed = true;
                RenderingChange = true;
                _previousAnchorPosition = transform.position;
            }

            
            if (_previousTangentMode != tangentMode)
            {
                Changed = true;
                RenderingChange = true;
                TangentChanged ();
                _previousTangentMode = tangentMode;
            }

            
            if (InTangent.localPosition != _previousInPosition)
            {
                Changed = true;
                RenderingChange = true;
                _previousInPosition = InTangent.localPosition;
                _masterTangent = InTangent;
                _slaveTangent = OutTangent;
                TangentChanged ();
                return;
            }

            if (OutTangent.localPosition != _previousOutPosition)
            {
                Changed = true;
                RenderingChange = true;
                _previousOutPosition = OutTangent.localPosition;
                _masterTangent = OutTangent;
                _slaveTangent = InTangent;
                TangentChanged ();
                return;
            }
        }

        
        void TangentChanged ()
        {
            
            switch (tangentMode)
            {
            case TangentMode.Free:
                break;

            case TangentMode.Mirrored:
                Vector3 mirroredOffset = _masterTangent.position - transform.position;
                _slaveTangent.position = transform.position - mirroredOffset;
                break;

            case TangentMode.Aligned:
                float distance = Vector3.Distance (_slaveTangent.position, transform.position);
                Vector3 alignedOffset = (_masterTangent.position - transform.position).normalized;
                _slaveTangent.position = transform.position - (alignedOffset * distance);
                break;
            }

            
            _previousInPosition = InTangent.localPosition;
            _previousOutPosition = OutTangent.localPosition;
        }

        
        void Initialize ()
        {
            _initialized = true;

            
            InTangent = transform.GetChild (0);
            OutTangent = transform.GetChild (1);
            Anchor = transform.GetChild (2); 

            
            _masterTangent = InTangent;
            _slaveTangent = OutTangent;

            
            Anchor.hideFlags = HideFlags.HideInHierarchy;

            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                if (Application.isEditor)
                {
                    item.sharedMaterial.hideFlags = HideFlags.HideInInspector;
                }
                else
                {
                    Destroy(item);
                }
            }

            foreach (var item in GetComponentsInChildren<MeshFilter>())
            {
                if (Application.isEditor)
                {
                    item.hideFlags = HideFlags.HideInInspector;
                }
                else
                {
                    Destroy(item);
                }
            }

            foreach (var item in GetComponentsInChildren<MeshRenderer>())
            {
                if (Application.isEditor)
                {
                    item.hideFlags = HideFlags.HideInInspector;
                }
                else
                {
                    Destroy(item);
                }
            }

            foreach (var item in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                if (Application.isEditor)
                {
                    item.hideFlags = HideFlags.HideInInspector;
                }
            }

            
            _previousTangentMode = tangentMode;
            _previousInPosition = InTangent.localPosition;
            _previousOutPosition = OutTangent.localPosition;
            _previousAnchorPosition = transform.position;
        }

        
        public void SetTangentStatus (bool inStatus, bool outStatus)
        {
            InTangent.gameObject.SetActive (inStatus);
            OutTangent.gameObject.SetActive (outStatus);
        }

        public void Tilt (Vector3 angles)
        {
            
            Quaternion rotation = transform.localRotation;
            transform.Rotate (angles);

            
            Vector3 inPosition = InTangent.position;
            Vector3 outPosition = OutTangent.position;

            
            
            InTangent.position = inPosition;
            OutTangent.position = outPosition;
        }
    }
}