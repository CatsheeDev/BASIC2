#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEditor;
using BASIC.UI;
using BASIC.UI.States;

public class BASICPackage
{
    public string packageName { get; set; }
    public string verCreated { get; set; }
    public string packageType { get; set; }
    public string packagePath { get; set; }

    public string packageInfo { get; set; }
}

public class BASICToolbox_PackageDatabase : BASICSingleton<BASICToolbox_PackageDatabase>
{
    public List<BASICPackage> packageInfos = new List<BASICPackage>();

    public TextAsset jsonAsset;

    public List<BASICPackage> downloadAllPackages()
    {
        StartCoroutine(DownloadPackages("https://raw.githubusercontent.com/CatsheeDev/BASIC2-Packages/main/packageList.json"));

        return packageInfos;
    }

    private IEnumerator DownloadPackages(string url)
    {
        using UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {
            EditorPrefs.SetString("BASIC_Packages", www.downloadHandler.text);
            extractPackages(www.downloadHandler.text);
        }
    }

    private void extractPackages(string packages)
    {
        packageInfos.Clear();
        List<BASICPackage> dPkg = JsonConvert.DeserializeObject<List<BASICPackage>>(packages);
        foreach (var packageInfo in dPkg)
        {
            packageInfos.Add(packageInfo);
        }
    }

    public void DownloadAndInstallPackage(string packageURL, BASICPackage ba)
    {
        StartCoroutine(DownloadPackage(packageURL, ba));
    }

    private IEnumerator DownloadPackage(string packageURL, BASICPackage ba)
    {
        using UnityWebRequest www = UnityWebRequest.Get(packageURL);
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                string packagePath = Path.Combine(Application.dataPath, "_BASIC/Temp", Path.GetFileName(packageURL));

                Directory.CreateDirectory(Path.GetDirectoryName(packagePath));
                File.WriteAllBytes(packagePath, www.downloadHandler.data);

                if (File.Exists(packagePath))
                {
                    AssetDatabase.ImportPackage(packagePath, true);
                    ImportPackageFromLocalFile(ba);
                    Debug.Log($"Imported {ba.packageName} successfully");
                }
                else
                {
                    Debug.LogError("Package file not found after download.");
                }
            }
        }
    }

    private void ImportPackageFromLocalFile(BASICPackage pack)
    {
        string packagesPath = Path.Combine(Application.dataPath, "packages.json");
        RootObject packageDatabase = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(packagesPath));
        packageDatabase.packages.Add(pack.packageName);

        string modifiedJsonContent = JsonConvert.SerializeObject(packageDatabase);
        File.WriteAllText(packagesPath, modifiedJsonContent);
        Debug.Log("Package imported successfully.");
    }

    public class RootObject
    {
        public List<string> packages { get; set; }
    }
}
#endif