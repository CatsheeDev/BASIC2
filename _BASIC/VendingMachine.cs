using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class VendingMachine : MonoBehaviour
{
    public int[] itemsCanVend;
}

#if UNITY_EDITOR
[CustomEditor(typeof(VendingMachine))]
public class VendingMachineEditor : Editor
{
    private SerializedProperty itemsCanVend;

    void OnEnable()
    {
        itemsCanVend = serializedObject.FindProperty("itemsCanVend");
    }
    public override void OnInspectorGUI()
    {

        Rect rect = GUILayoutUtility.GetLastRect();
        Vector2 tooltipPosition = new Vector2(rect.xMax + 10, rect.y);

        if (itemsCanVend.arraySize > 0)
        {
            EditorGUILayout.HelpBox("-1 = payphone", MessageType.Info);
        }

        DrawDefaultInspector();
    }
}
#endif