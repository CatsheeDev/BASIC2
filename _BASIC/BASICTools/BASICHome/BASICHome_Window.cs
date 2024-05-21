#if UNITY_EDITOR
using BASIC.UI;
using BASIC.UI.Home;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

[InitializeOnLoad]
public class BASICHome_Window : MonoBehaviour
{
    private static BASICHome_Window Instance;

    [InitializeOnLoadMethod]
    private static void onEditorReload()
    {
        if (Instance != null)
        {
            DestroyImmediate(Instance.gameObject);
        }

        GameObject newGO = new GameObject("TEMP_verCheck");
        Instance = newGO.AddComponent<BASICHome_Window>();
        
        if (EditorPrefs.GetBool("BASIC_showUpdate"))
        {
            Instance.CheckVersion();  
        } else
        {
            DestroyImmediate(Instance.gameObject);
        }
    }

    private void CheckVersion()
    {
        string remoteVersionUrl = "https://raw.githubusercontent.com/CatsheeDev/BASIC2/master/version.txt";
        string localVersionPath = "version.txt";

        StartCoroutine(CheckRemoteVersion(remoteVersionUrl, localVersionPath));
    }

    private IEnumerator CheckRemoteVersion(string remoteVersionUrl, string localVersionPath)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(remoteVersionUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to find cur version" + www.error);
            }
            else
            {
                string remoteVersion = www.downloadHandler.text;
                string localVersion = File.ReadAllText(localVersionPath);

                if (remoteVersion != localVersion)
                {
                    BASIC_UIBASE.createWindow<BASICHome_ActuallyWindow>("BASIC Home");

                    BASICHome_ActuallyWindow.Instance.stateManager.SetState(new BASICHome_EditorWindow_Update()); 
                }

            }
        } 

        DestroyImmediate(Instance.gameObject); 
    }
}
#endif