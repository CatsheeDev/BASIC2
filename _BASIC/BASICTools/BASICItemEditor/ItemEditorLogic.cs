#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ItemEditorLogic : BASICSingleton<ItemEditorLogic>
{
    public GameControllerScript _controllerScript;
    public ItemProfile2 _itemProfile;

    public void CreateScript(string scriptName, GameObject targetGameObject)
    {
        string path = "Assets/Scripts/ItemScripts/" + scriptName + ".cs";

        if (!File.Exists(path))
        {
            string script = $@"
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class {scriptName} : ItemBase
{{
    public override void Pickup()
    {{
        base.Pickup();
    }}

    public override void Use()
    {{
        base.Use();
    }}
}}";
            File.WriteAllText(path, script);
            AssetDatabase.Refresh();
        }
        else
        {
        }
    }
}
#endif