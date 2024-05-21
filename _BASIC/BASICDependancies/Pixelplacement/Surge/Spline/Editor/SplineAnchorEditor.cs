








using UnityEditor;
using UnityEngine;

namespace Pixelplacement
{
    [CustomEditor(typeof(SplineAnchor))]
    public class SplineAnchorEditor : Editor
    {
        
        void OnSceneGUI ()
        {
            
            if (Tools.pivotMode == PivotMode.Center)
            {
                Tools.pivotMode = PivotMode.Pivot;
            }
        }

        
        [DrawGizmo(GizmoType.Selected)]
        static void RenderCustomGizmo(Transform objectTransform, GizmoType gizmoType)
        {
            if (objectTransform.parent != null)
            {
                SplineEditor.RenderCustomGizmo(objectTransform.parent, gizmoType);
            }
        }
    }
}