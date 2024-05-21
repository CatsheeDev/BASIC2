








using UnityEditor;
using UnityEngine;

namespace Pixelplacement
{
    [CustomEditor(typeof(SplineTangent))]
    public class SplineTangentEditor : Editor
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
            if (objectTransform.parent != null && objectTransform.parent.parent != null)
            {
                SplineEditor.RenderCustomGizmo(objectTransform.parent.parent, gizmoType);
            }
        }
    }
}