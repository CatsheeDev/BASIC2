using UnityEditor;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public int[] itemsCanVend;
}

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
        DrawDefaultInspector();

        Rect rect = GUILayoutUtility.GetLastRect();
        Vector2 tooltipPosition = new Vector2(rect.xMax + 10, rect.y);

        if (itemsCanVend.arraySize > 0)
        {
            EditorGUILayout.HelpBox("-1 = payphone", MessageType.Info);
        }
    }
}