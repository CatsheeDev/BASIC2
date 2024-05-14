using BASIC.UI;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
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

        Debug.Log("init ver check");
        Instance.CheckVersion();
    }

    private void CheckVersion()
    {
        string remoteVersionUrl = "https://raw.githubusercontent.com/yourusername/yourrepository/master/version.txt";
        string localVersionPath = "path/to/your/local/version.txt";

        StartCoroutine(CheckRemoteVersion(remoteVersionUrl, localVersionPath));
    }

    private static IEnumerator CheckRemoteVersion(string remoteVersionUrl, string localVersionPath)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(remoteVersionUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to find Version" + www.error);
            }
            else
            {
                string remoteVersion = www.downloadHandler.text;
                string localVersion = File.ReadAllText(localVersionPath);

                if (remoteVersion == localVersion)
                {
                    Debug.Log("");
                }
                else
                {
                    Debug.Log("Versions do not match???? fuck???");
                    BASIC_UIBASE.createWindow<BASICHome_ActuallyWindow>("BASIC Home");
                }
            }
        }

        DestroyImmediate(Instance.gameObject); 
    }
}
